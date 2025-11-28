using MediatR;
using Audit360.Application.Features.Dto.Audits;
using System.Collections.Generic;

namespace Audit360.Application.Features.Audits.Queries
{
    public record GetAuditFinalizedSummaryQuery : IRequest<IEnumerable<AuditFinalizedSummaryReadDto>>;
}
