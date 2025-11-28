using Audit360.Application.Features.Dto.Roles;
using Audit360.Application.Features.Roles.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class RoleHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IRoleReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<Role> { new() { Id = 1, Name = "R" } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<RoleReadDto> { new(1, "R", "d") };
            mockMapper.Setup(m => m.Map<IEnumerable<RoleReadDto>>(It.IsAny<IEnumerable<Role>>())).Returns(dtoList);

            var handler = new RoleQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Roles.Queries.GetRolesQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Command_Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IRoleWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var createDto = new Audit360.Application.Features.Dto.Roles.RoleWriteDto("R", "d");
            var role = new Role { Name = createDto.Name, Description = createDto.Description };
            mockMapper.Setup(m => m.Map<Role>(It.IsAny<Audit360.Application.Features.Dto.Roles.RoleWriteDto>())).Returns(role);

            var cmdHandler = new RoleCommandHandler(mockRepo.Object, mockMapper.Object);
            await cmdHandler.Handle(new Audit360.Application.Features.Roles.Commands.CreateRoleCommand(createDto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Role>()), Times.Once);

            await cmdHandler.Handle(new Audit360.Application.Features.Roles.Commands.UpdateRoleCommand(3, createDto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Role>(x => x.Id == 3)), Times.Once);

            await cmdHandler.Handle(new Audit360.Application.Features.Roles.Commands.DeleteRoleCommand(5), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(5), Times.Once);
        }
    }
}
