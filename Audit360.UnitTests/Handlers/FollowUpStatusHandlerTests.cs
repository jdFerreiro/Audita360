using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.FollowUpStatuses.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.UnitTests.Handlers
{
    public class FollowUpStatusHandlerTests
    {
        [Fact]
        public async Task Command_Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IFollowUpStatusWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new FollowUpStatusWriteDto("desc");
            var entity = new FollowUpStatus { Description = dto.Description };
            mockMapper.Setup(m => m.Map<FollowUpStatus>(It.IsAny<FollowUpStatusWriteDto>())).Returns(entity);

            var handler = new FollowUpStatusCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.FollowUpStatuses.Commands.CreateFollowUpStatusCommand(dto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<FollowUpStatus>()), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FollowUpStatuses.Commands.UpdateFollowUpStatusCommand(2, dto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<FollowUpStatus>(x => x.Id == 2)), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FollowUpStatuses.Commands.DeleteFollowUpStatusCommand(3), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}
