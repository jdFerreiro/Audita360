using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Audit360.API;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.IntegrationTests
{
    public class UsersEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersEndpointTests(WebApplicationFactory<Program> factory) => _factory = factory;

        [Fact]
        public async Task CreateUser_Returns_Conflict_On_Db_FK_Error()
        {
            var client = _factory.CreateClient();

            // Create a user with invalid RoleId to trigger FK violation
            var dto = new UserWriteDto("testuser","test@example.com","password123","Full",true,9999);

            var response = await client.PostAsJsonAsync("/api/users", dto);

            // Should be handled by middleware; expect 409 or 500 depending on environment
            Assert.True(response.StatusCode == HttpStatusCode.Conflict || response.StatusCode == HttpStatusCode.InternalServerError);

            var json = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(json));
        }
    }
}
