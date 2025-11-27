using MediatR;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.Application.Features.Audits.Commands
{
    /// <summary>
    /// Comando para crear una auditoría.
    /// </summary>
    /// <param name="Audit">Datos de la auditoría a crear.</param>
    public record CreateAuditCommand(AuditWriteDto Audit) : IRequest<Unit>;
}