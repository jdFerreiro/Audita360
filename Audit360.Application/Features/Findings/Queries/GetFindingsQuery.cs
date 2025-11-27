using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.Application.Features.Findings.Queries
{
    public record GetFindingsQuery : IRequest<IEnumerable<FindingReadDto>>;
}