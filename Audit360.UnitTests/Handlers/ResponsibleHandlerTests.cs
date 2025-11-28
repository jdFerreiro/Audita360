using Audit360.Application.Features.Dto.Responsibles;
using Audit360.Application.Features.Responsibles.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class ResponsibleHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IResponsibleReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<Responsible> { new() { Id = 1, Name = "R", Email = "r@e.com", Area = "Area" } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<ResponsibleReadDto> { new(1, "R", "r@e.com") };
            mockMapper.Setup(m => m.Map<IEnumerable<ResponsibleReadDto>>(It.IsAny<IEnumerable<Responsible>>())).Returns(dtoList);

            var handler = new ResponsibleQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.Responsibles.Queries.GetResponsiblesQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }
    }
}
