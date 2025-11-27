using System.Threading.Tasks;
using System.Threading;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.Users.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Interfaces;
using Audit360.Application.Configurations;
using Audit360.Application.Features.Users.Commands;
using Audit360.Domain.Entities;

namespace Audit360.UnitTests.Handlers
{
    public class AuthenticateUserHandlerTests
    {
        [Fact]
        public async Task Authenticate_ReturnsToken_WhenValid()
        {
            var mockRepo = new Mock<IUserReadRepository>();
            var mockPassword = new Mock<IPasswordService>();

            var user = new User { Id = 1, Username = "user1", Email = "a@b.com", PasswordHash = "h", FullName = "Full", RoleId = 1, Role = new Role { Id = 1, Name = "Admin" } };
            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            mockPassword.Setup(p => p.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var jwtOptions = new JwtOptions { Secret = "verysecretkeyverysecretkey", Issuer = "issuer", Audience = "aud" };

            var handler = new AuthenticateUserHandler(mockRepo.Object, mockPassword.Object, jwtOptions);

            var result = await handler.Handle(new AuthenticateUserCommand("a@b.com", "pwd"), CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.Token));
        }
    }
}
