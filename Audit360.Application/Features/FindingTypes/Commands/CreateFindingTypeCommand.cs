using MediatR;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.Application.Features.FindingTypes.Commands
{
    public record CreateFindingTypeCommand(FindingTypeWriteDto FindingType) : IRequest;
}