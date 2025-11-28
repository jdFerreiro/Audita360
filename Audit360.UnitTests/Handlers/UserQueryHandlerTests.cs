using Audit360.Application.Features.Dto.Users;
using Audit360.Application.Features.Users.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class UserQueryHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IUserReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<User> { new() { Id = 1, Username = "u", Email = "e@x.com", PasswordHash = "h", FullName = "Full", RoleId = 1, Role = new Role { Id = 1, Name = "Role" } } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<UserReadDto> { new(1, "u", "e@x.com", "Full", true, 1, System.DateTime.UtcNow) };
            mockMapper.Setup(m => m.Map<IEnumerable<UserReadDto>>(It.IsAny<IEnumerable<User>>())).Returns(dtoList);

            var handler = new UserQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Users.Queries.GetUsersQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }
    }
}
