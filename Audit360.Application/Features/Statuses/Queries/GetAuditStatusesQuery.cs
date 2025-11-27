using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.Application.Features.Statuses.Queries
{
    public record GetAuditStatusesQuery : IRequest<IEnumerable<AuditStatusReadDto>>;
}