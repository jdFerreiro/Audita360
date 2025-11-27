using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.Application.Features.FindingSeverities.Queries
{
    public record GetFindingSeveritiesQuery : IRequest<IEnumerable<FindingSeverityReadDto>>;
}