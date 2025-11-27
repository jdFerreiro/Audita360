using MediatR;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.Application.Features.Audits.Commands
{
    public record UpdateAuditCommand(int Id, AuditWriteDto Audit) : IRequest;
}