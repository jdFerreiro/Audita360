using MediatR;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.Application.Features.FindingTypes.Queries
{
    public record GetFindingTypeByIdQuery(int Id) : IRequest<FindingTypeReadDto?>;
}