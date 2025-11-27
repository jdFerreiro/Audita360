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
            catch (Exception ex) when (IsSqlLike(ex, out int number, out string detail))
            {
                _logger.LogError(ex, "Database SQL error occurred (simulated or real). Number: {Number}", number);

                int status = number switch
                {
                    547 => StatusCodes.Status409Conflict, // FK violation
                    2627 => StatusCodes.Status409Conflict, // Unique constraint (PK) violation
                    2601 => StatusCodes.Status409Conflict, // Duplicated key
                    _ => StatusCodes.Status500InternalServerError
                };

                var message = number switch
                {
                    547 => "Database constraint violation: foreign key or check constraint failure.",
                    2627 => "Database constraint violation: unique key or primary key already exists.",
                    2601 => "Database constraint violation: duplicated key.",
                    _ => "Database error occurred."
                };

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";
                var payload = JsonSerializer.Serialize(new { error = message, detail });
                await context.Response.WriteAsync(payload);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Entity Framework update error occurred.");

                // Try to inspect inner exception for SqlException or simulated sql-like exception
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
                else if (IsSqlLike(dbEx.InnerException, out int num, out string det))
                {
                    // Handle simulated inner SQL-like exceptions
                    int status = num switch
                    {
                        547 => StatusCodes.Status409Conflict,
                        2627 => StatusCodes.Status409Conflict,
                        2601 => StatusCodes.Status409Conflict,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    var message = num switch
                    {
                        547 => "Database constraint violation: foreign key or check constraint failure.",
                        2627 => "Database constraint violation: unique key or primary key already exists.",
                        2601 => "Database constraint violation: duplicated key.",
                        _ => "Database error occurred."
                    };

                    context.Response.StatusCode = status;
                    context.Response.ContentType = "application/json";
                    var payload = JsonSerializer.Serialize(new { error = message, detail = det });
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

        private static bool IsSqlLike(Exception ex, out int number, out string detail)
        {
            number = 0;
            detail = ex?.Message ?? string.Empty;

            if (ex == null) return false;

            // Real SqlException
            if (ex is SqlException sql)
            {
                number = sql.Number;
                detail = sql.Message;
                return true;
            }

            // Simulated via exception.Data["SqlNumber"]
            if (ex.Data != null && ex.Data.Contains("SqlNumber") && ex.Data["SqlNumber"] is int n)
            {
                number = n;
                detail = ex.Message;
                return true;
            }

            return false;
        }
    }
}
