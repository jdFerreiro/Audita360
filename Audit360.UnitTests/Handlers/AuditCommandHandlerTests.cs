using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.Audits.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Dto.Audits;
using Audit360.Application.Features.Audits.Commands;

namespace Audit360.UnitTests.Handlers
{
    public class AuditCommandHandlerTests
    {
        [Fact]
        public async Task Create_CallsRepositoryWithMappedEntity()
        {
            var mockRepo = new Mock<IAuditWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new Audit360.Application.Features.Dto.Audits.AuditWriteDto("T", "A", System.DateTime.UtcNow, null, 1, 1);
            var entity = new Audit { Title = dto.Title, Area = dto.Area, StartDate = dto.StartDate, Status = new AuditStatus { Id = dto.StatusId, Description = string.Empty }, ResponsibleId = dto.ResponsibleId };
            mockMapper.Setup(m => m.Map<Audit>(It.IsAny<Audit360.Application.Features.Dto.Audits.AuditWriteDto>())).Returns(entity);

            var handler = new AuditCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new CreateAuditCommand(dto), CancellationToken.None);

            mockRepo.Verify(r => r.CreateAsync(It.Is<Audit>(a => a.Title == "T")), Times.Once);
        }

        [Fact]
        public async Task Update_SetsIdAndCallsUpdate()
        {
            var mockRepo = new Mock<IAuditWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new Audit360.Application.Features.Dto.Audits.AuditWriteDto("T2", "A2", System.DateTime.UtcNow, null, 1, 1);
            var entity = new Audit { Title = dto.Title, Area = dto.Area, StartDate = dto.StartDate, Status = new AuditStatus { Id = dto.StatusId, Description = string.Empty }, ResponsibleId = dto.ResponsibleId };
            mockMapper.Setup(m => m.Map<Audit>(It.IsAny<Audit360.Application.Features.Dto.Audits.AuditWriteDto>())).Returns(entity);

            var handler = new AuditCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new UpdateAuditCommand(9, dto), CancellationToken.None);

            mockRepo.Verify(r => r.UpdateAsync(It.Is<Audit>(a => a.Id == 9)), Times.Once);
        }

        [Fact]
        public async Task Delete_CallsRepositoryDelete()
        {
            var mockRepo = new Mock<IAuditWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var handler = new AuditCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new DeleteAuditCommand(7), CancellationToken.None);

            mockRepo.Verify(r => r.DeleteAsync(7), Times.Once);
        }
    }
}
