using MediatR;
using Audit360.Application.Features.Dto.Audits;

namespace Audit360.Application.Features.Audits.Commands
{
    /// <summary>
    /// Comando para actualizar una auditoría.
    /// </summary>
    /// <param name="Id">Identificador de la auditoría a actualizar.</param>
    /// <param name="Audit">Datos actualizados de la auditoría.</param>
    public record UpdateAuditCommand(int Id, AuditWriteDto Audit) : IRequest<MediatR.Unit>;
}