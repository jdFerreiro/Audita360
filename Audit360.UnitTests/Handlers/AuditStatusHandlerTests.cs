using Audit360.Application.Features.Dto.Statuses;
using Audit360.Application.Features.Statuses.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class AuditStatusHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IAuditStatusReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<AuditStatus> { new() { Id = 1, Description = "S" } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<AuditStatusReadDto> { new(1, "S") };
            mockMapper.Setup(m => m.Map<IEnumerable<AuditStatusReadDto>>(It.IsAny<IEnumerable<AuditStatus>>())).Returns(dtoList);

            var handler = new AuditStatusQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Statuses.Queries.GetAuditStatusesQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Command_Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IAuditStatusWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new Audit360.Application.Features.Dto.Statuses.AuditStatusWriteDto("S");
            var entity = new AuditStatus { Description = dto.Description };
            mockMapper.Setup(m => m.Map<AuditStatus>(It.IsAny<Audit360.Application.Features.Dto.Statuses.AuditStatusWriteDto>())).Returns(entity);

            var handler = new AuditStatusCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.Statuses.Commands.CreateAuditStatusCommand(dto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<AuditStatus>()), Times.Once);

            await handler.Handle(new Audit360.Application.Features.Statuses.Commands.UpdateAuditStatusCommand(2, dto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<AuditStatus>(x => x.Id == 2)), Times.Once);

            await handler.Handle(new Audit360.Application.Features.Statuses.Commands.DeleteAuditStatusCommand(3), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}
