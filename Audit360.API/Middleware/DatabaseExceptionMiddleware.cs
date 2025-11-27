using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Audit360.API.Middleware
{
    public class DatabaseExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DatabaseExceptionMiddleware> _logger;

        public DatabaseExceptionMiddleware(RequestDelegate next, ILogger<DatabaseExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Database SQL error occurred.");

                int status = sqlEx.Number switch
                {
                    547 => StatusCodes.Status409Conflict, // FK violation
                    2627 => StatusCodes.Status409Conflict, // Unique constraint (PK) violation
                    2601 => StatusCodes.Status409Conflict, // Duplicated key
                    _ => StatusCodes.Status500InternalServerError
                };

                var message = sqlEx.Number switch
                {
                    547 => "Database constraint violation: foreign key or check constraint failure.",
                    2627 => "Database constraint violation: unique key or primary key already exists.",
                    2601 => "Database constraint violation: duplicated key.",
                    _ => "Database error occurred."
                };

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";
                var payload = JsonSerializer.Serialize(new { error = message, detail = sqlEx.Message });
                await context.Response.WriteAsync(payload);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Entity Framework update error occurred.");

                // Try to inspect inner exception for SqlException
                if (dbEx.InnerException is SqlException innerSql)
                {
                    int status = innerSql.Number switch
                    {
                        547 => StatusCodes.Status409Conflict,
                        2627 => StatusCodes.Status409Conflict,
                        2601 => StatusCodes.Status409Conflict,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    var message = innerSql.Number switch
                    {
                        547 => "Database constraint violation: foreign key or check constraint failure.",
                        2627 => "Database constraint violation: unique key or primary key already exists.",
                        2601 => "Database constraint violation: duplicated key.",
                        _ => "Database error occurred."
                    };

                    context.Response.StatusCode = status;
                    context.Response.ContentType = "application/json";
                    var payload = JsonSerializer.Serialize(new { error = message, detail = innerSql.Message });
                    await context.Response.WriteAsync(payload);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var payload = JsonSerializer.Serialize(new { error = "A database error occurred.", detail = dbEx.Message });
                    await context.Response.WriteAsync(payload);
                }
            }
        }
    }
}
