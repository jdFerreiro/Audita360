using Audit360.Application.Features.Dto.FindingSeverities;
using Audit360.Application.Features.FindingSeverities.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class FindingSeverityHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IFindingSeverityReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<FindingSeverity> { new() { Id = 1, Description = "S" } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<FindingSeverityReadDto> { new(1, "S") };
            mockMapper.Setup(m => m.Map<IEnumerable<FindingSeverityReadDto>>(It.IsAny<IEnumerable<FindingSeverity>>())).Returns(dtoList);

            var handler = new FindingSeverityQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.FindingSeverities.Queries.GetFindingSeveritiesQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Command_Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IFindingSeverityWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var createDto = new Audit360.Application.Features.Dto.FindingSeverities.FindingSeverityWriteDto("S");
            var entity = new FindingSeverity { Description = createDto.Description };
            mockMapper.Setup(m => m.Map<FindingSeverity>(It.IsAny<Audit360.Application.Features.Dto.FindingSeverities.FindingSeverityWriteDto>())).Returns(entity);

            var handler = new FindingSeverityCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.FindingSeverities.Commands.CreateFindingSeverityCommand(createDto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<FindingSeverity>()), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FindingSeverities.Commands.UpdateFindingSeverityCommand(2, createDto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<FindingSeverity>(x => x.Id == 2)), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FindingSeverities.Commands.DeleteFindingSeverityCommand(3), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}
