using Audit360.Application.Features.Audits.Handlers;
using Audit360.Application.Features.Dto.Audits;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class AuditQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsMappedDtos()
        {
            var mockRepo = new Mock<IAuditReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var audits = new List<Audit> {
                new() {
                    Id = 1,
                    Title = "T",
                    Area = "A",
                    StartDate = System.DateTime.UtcNow,
                    Status = new AuditStatus { Id = 1, Description = "S" },
                    ResponsibleId = 1,
                    Responsible = new Responsible { Id = 1, Name = "R", Email = "r@e.com", Area = "Area" }
                }
            };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(audits);

            var dtoList = new List<AuditReadDto> { new(1, "T", "A", System.DateTime.UtcNow, null, 0, 0) };
            mockMapper.Setup(m => m.Map<IEnumerable<AuditReadDto>>(It.IsAny<IEnumerable<Audit>>())).Returns(dtoList);

            var handler = new AuditQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Audits.Queries.GetAuditsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Handle_GetById_ReturnsNullWhenNotFound()
        {
            var mockRepo = new Mock<IAuditReadRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync((Audit?)null);

            var handler = new AuditQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Audits.Queries.GetAuditByIdQuery(5), CancellationToken.None);

            Assert.Null(result);
        }
    }
}
