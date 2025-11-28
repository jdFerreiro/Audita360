using Audit360.Application.Features.Dto.FindingTypes;
using Audit360.Application.Features.FindingTypes.Handlers;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using Moq;

namespace Audit360.UnitTests.Handlers
{
    public class FindingTypeHandlerTests
    {
        [Fact]
        public async Task Query_ReturnsMapped()
        {
            var mockRepo = new Mock<IFindingTypeReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var list = new List<FindingType> { new() { Id = 1, Description = "FT" } };
            mockRepo.Setup(r => r.GetListAsync()).ReturnsAsync(list);

            var dtoList = new List<FindingTypeReadDto> { new(1, "FT") };
            mockMapper.Setup(m => m.Map<IEnumerable<FindingTypeReadDto>>(It.IsAny<IEnumerable<FindingType>>())).Returns(dtoList);

            var handler = new FindingTypeQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new Audit360.Application.Features.FindingTypes.Queries.GetFindingTypesQuery(), CancellationToken.None);

            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task Command_Create_Update_Delete_CallsRepo()
        {
            var mockRepo = new Mock<IFindingTypeWriteRepository>();
            var mockMapper = new Mock<IMapper>();

            var createDto = new Audit360.Application.Features.Dto.FindingTypes.FindingTypeWriteDto("FT");
            var entity = new FindingType { Description = createDto.Description };
            mockMapper.Setup(m => m.Map<FindingType>(It.IsAny<Audit360.Application.Features.Dto.FindingTypes.FindingTypeWriteDto>())).Returns(entity);

            var handler = new FindingTypeCommandHandler(mockRepo.Object, mockMapper.Object);
            await handler.Handle(new Audit360.Application.Features.FindingTypes.Commands.CreateFindingTypeCommand(createDto), CancellationToken.None);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<FindingType>()), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FindingTypes.Commands.UpdateFindingTypeCommand(2, createDto), CancellationToken.None);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<FindingType>(x => x.Id == 2)), Times.Once);

            await handler.Handle(new Audit360.Application.Features.FindingTypes.Commands.DeleteFindingTypeCommand(3), CancellationToken.None);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}
