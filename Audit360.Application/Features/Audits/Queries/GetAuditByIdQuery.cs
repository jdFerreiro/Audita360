using MediatR;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.Application.Features.Audits.Queries
{
    public record GetAuditByIdQuery(int Id) : IRequest<AuditReadDto?>;
}