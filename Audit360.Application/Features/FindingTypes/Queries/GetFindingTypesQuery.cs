using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.Application.Features.FindingTypes.Queries
{
    public record GetFindingTypesQuery : IRequest<IEnumerable<FindingTypeReadDto>>;
}