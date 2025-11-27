using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.Users.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Interfaces;
using Audit360.Application.Features.Dto.Users;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Users.Commands;

namespace Audit360.UnitTests.Handlers
{
    public class UserCommandHandlerTests
    {
        [Fact]
        public async Task Create_HashesPassword_And_CallsRepository()
        {
            var mockRepo = new Mock<IUserWriteRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockPassword = new Mock<IPasswordService>();

            var dto = new UserWriteDto("user1","user1@example.com","pass123","Full Name",true,1);
            var userEntity = new User { Username = dto.Username, Email = dto.Email, PasswordHash = string.Empty, FullName = dto.FullName, IsActive = dto.IsActive, RoleId = dto.RoleId, Role = new Role { Id = dto.RoleId, Name = "Role" } };

            mockMapper.Setup(m => m.Map<User>(It.IsAny<UserWriteDto>())).Returns(userEntity);
            mockPassword.Setup(p => p.HashPassword(dto.Password)).Returns("hashed");

            var handler = new UserCommandHandler(mockRepo.Object, mockMapper.Object, mockPassword.Object);

            await handler.Handle(new CreateUserCommand(dto), CancellationToken.None);

            mockPassword.Verify(p => p.HashPassword(dto.Password), Times.Once);
            mockRepo.Verify(r => r.CreateAsync(It.Is<User>(u => u.PasswordHash == "hashed")), Times.Once);
        }

        [Fact]
        public async Task Update_HashesPassword_WhenProvided()
        {
            var mockRepo = new Mock<IUserWriteRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockPassword = new Mock<IPasswordService>();

            var dto = new UserWriteDto("user1","user1@example.com","newpass","Full Name",true,1);
            var userEntity = new User { Username = dto.Username, Email = dto.Email, PasswordHash = string.Empty, FullName = dto.FullName, IsActive = dto.IsActive, RoleId = dto.RoleId, Role = new Role { Id = dto.RoleId, Name = "Role" } };

            mockMapper.Setup(m => m.Map<User>(It.IsAny<UserWriteDto>())).Returns(userEntity);
            mockPassword.Setup(p => p.HashPassword(dto.Password)).Returns("hashednew");

            var handler = new UserCommandHandler(mockRepo.Object, mockMapper.Object, mockPassword.Object);

            await handler.Handle(new UpdateUserCommand(5, dto), CancellationToken.None);

            mockPassword.Verify(p => p.HashPassword(dto.Password), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<User>(u => u.PasswordHash == "hashednew")), Times.Once);
        }
    }
}
