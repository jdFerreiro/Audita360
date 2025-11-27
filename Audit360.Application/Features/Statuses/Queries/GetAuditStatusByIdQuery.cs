using MediatR;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.Application.Features.Statuses.Queries
{
    public record GetAuditStatusByIdQuery(int Id) : IRequest<AuditStatusReadDto?>;
}