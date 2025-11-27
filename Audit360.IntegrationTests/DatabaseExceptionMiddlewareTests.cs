using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Audit360.API.Middleware;

namespace Audit360.IntegrationTests
{
    public class DatabaseExceptionMiddlewareTests
    {
        [Fact]
        public async Task SqlException_IsHandled_Returns_Conflict()
        {
            // Arrange
            var logger = new NullLogger<DatabaseExceptionMiddleware>();
            RequestDelegate next = (HttpContext ctx) => throw CreateFakeSqlException(547, "FK violation test");
            var middleware = new DatabaseExceptionMiddleware(next, logger);

            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream();

            // Act
            await middleware.InvokeAsync(context);
            context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
            var body = await new System.IO.StreamReader(context.Response.Body).ReadToEndAsync();

            // Assert
            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal((int)HttpStatusCode.Conflict, context.Response.StatusCode);
            var json = JsonSerializer.Deserialize<JsonElement>(body);
            Assert.True(json.TryGetProperty("error", out _));
            Assert.True(json.TryGetProperty("detail", out _));
        }

        [Fact]
        public async Task DbUpdateException_WithInnerSqlException_IsHandled()
        {
            var logger = new NullLogger<DatabaseExceptionMiddleware>();
            var innerSql = CreateFakeSqlException(2627, "Unique constraint test");
            RequestDelegate next = (HttpContext ctx) => throw new DbUpdateException("EF update failed", innerSql);
            var middleware = new DatabaseExceptionMiddleware(next, logger);

            var context = new DefaultHttpContext();
            context.Response.Body = new System.IO.MemoryStream();

            await middleware.InvokeAsync(context);
            context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
            var body = await new System.IO.StreamReader(context.Response.Body).ReadToEndAsync();

            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal((int)HttpStatusCode.Conflict, context.Response.StatusCode);
            var json = JsonSerializer.Deserialize<JsonElement>(body);
            Assert.True(json.TryGetProperty("error", out _));
            Assert.True(json.TryGetProperty("detail", out _));
        }

        // Helper to create a fake exception that simulates a SqlException by holding SqlNumber in Data
        private static Exception CreateFakeSqlException(int number, string message)
        {
            var ex = new Exception(message);
            ex.Data["SqlNumber"] = number;
            return ex;
        }
    }
}
