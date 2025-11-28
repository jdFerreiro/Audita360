using Audit360.Application.Features.Dto.Findings;
using Audit360.Application.Features.Findings.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class FindingQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GetList_ReturnsMapped()
        {
            var mockRepo = new Mock<IFindingReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<Finding>
            {
                new()
                {
                    Id = 1,
                    Description = "d",
                    Type = new FindingType { Id = 1, Description = "Type1" },
                    Severity = default!
                }
            };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<FindingReadDto> { new(1, "d", System.DateTime.UtcNow, 1, 1, 1) };
            mockMapper.Setup(m => m.Map<IEnumerable<FindingReadDto>>(It.IsAny<IEnumerable<Finding>>())).Returns(dtoList);

            var handler = new FindingQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Findings.Queries.GetFindingsQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Handle_GetById_ReturnsNullWhenNotFound()
        {
            var mockRepo = new Mock<IFindingReadRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync((Finding?)null);

            var handler = new FindingQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Findings.Queries.GetFindingByIdQuery(5), CancellationToken.None);

            Assert.Null(result);
        }
    }
}
