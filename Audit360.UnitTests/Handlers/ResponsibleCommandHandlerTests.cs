using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.Responsibles.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.UnitTests.Handlers
{
    public class ResponsibleCommandHandlerTests
    {
        [Fact]
        public async Task Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IResponsibleWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var createDto = new ResponsibleWriteDto("R","r@e.com","Area");
            var entity = new Responsible { Name = createDto.Name, Email = createDto.Email, Area = createDto.Area };
            mockMapper.Setup(m => m.Map<Responsible>(It.IsAny<ResponsibleWriteDto>())).Returns(entity);

            var handler = new ResponsibleCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.Responsibles.Commands.CreateResponsibleCommand(createDto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Responsible>()), Times.Once);

            await handler.Handle(new Audit360.Application.Features.Responsibles.Commands.UpdateResponsibleCommand(5, createDto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Responsible>(x => x.Id == 5)), Times.Once);

            await handler.Handle(new Audit360.Application.Features.Responsibles.Commands.DeleteResponsibleCommand(6), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(6), Times.Once);
        }
    }
}
