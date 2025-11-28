using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using AutoMapper;
using Audit360.Application.Features.Findings.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Findings.Commands;
using Audit360.Domain.Entities;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.UnitTests.Handlers
{
    public class FindingCommandHandlerTests
    {
        [Fact]
        public async Task Create_CallsCreate()
        {
            var mockRepo = new Mock<IFindingWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new FindingWriteDto("d", System.DateTime.UtcNow, 1, 1, 1);
            var entity = new Finding { Description = dto.Description, Date = dto.Date, Type = new FindingType { Id = dto.FindingTypeId, Description = string.Empty }, Severity = new FindingSeverity { Id = dto.SeverityId, Description = string.Empty }, AuditId = dto.AuditId };
            mockMapper.Setup(m => m.Map<Finding>(It.IsAny<FindingWriteDto>())).Returns(entity);

            var handler = new FindingCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new CreateFindingCommand(dto), CancellationToken.None);

            mockRepo.Verify(r => r.CreateAsync(It.Is<Finding>(f => f.Description == "d")), Times.Once);
        }

        [Fact]
        public async Task Update_CallsUpdateWithId()
        {
            var mockRepo = new Mock<IFindingWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var dto = new FindingWriteDto("d2", System.DateTime.UtcNow, 1, 1, 1);
            var entity = new Finding { Description = dto.Description, Date = dto.Date, Type = new FindingType { Id = dto.FindingTypeId, Description = string.Empty }, Severity = new FindingSeverity { Id = dto.SeverityId, Description = string.Empty }, AuditId = dto.AuditId };
            mockMapper.Setup(m => m.Map<Finding>(It.IsAny<FindingWriteDto>())).Returns(entity);

            var handler = new FindingCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.Findings.Commands.UpdateFindingCommand(11, dto), CancellationToken.None);

            mockRepo.Verify(r => r.UpdateAsync(It.Is<Finding>(f => f.Id == 11)), Times.Once);
        }

        [Fact]
        public async Task Delete_CallsDelete()
        {
            var mockRepo = new Mock<IFindingWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var handler = new FindingCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.Findings.Commands.DeleteFindingCommand(4), CancellationToken.None);

            mockRepo.Verify(r => r.DeleteAsync(4), Times.Once);
        }
    }
}
