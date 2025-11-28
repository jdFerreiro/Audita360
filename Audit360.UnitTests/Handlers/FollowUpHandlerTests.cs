using Audit360.Application.Features.Dto.FollowUps;
using Audit360.Application.Features.FollowUps.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class FollowUpHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IFollowUpReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<FollowUp> { new() { Id = 1, Description = "d", CommitmentDate = System.DateTime.UtcNow, FollowUpStatusId = 1, FindingId = 1 } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<FollowUpReadDto> { new(1, "d", System.DateTime.UtcNow, 1, 1) };
            mockMapper.Setup(m => m.Map<IEnumerable<FollowUpReadDto>>(It.IsAny<IEnumerable<FollowUp>>())).Returns(dtoList);

            var handler = new FollowUpQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.FollowUps.Queries.GetFollowUpsQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }
    }
}
