using MediatR;

namespace Audit360.Application.Features.Audits.Commands
{
    public record DeleteAuditCommand(int Id) : IRequest<MediatR.Unit>;
}