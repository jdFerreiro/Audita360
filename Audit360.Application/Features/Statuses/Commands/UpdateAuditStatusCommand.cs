using MediatR;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.Application.Features.Statuses.Commands
{
    /// <summary>
    /// Comando para actualizar un estado de auditoría.
    /// </summary>
    /// <param name="Id">Identificador del estado a actualizar.</param>
    /// <param name="Status">Datos actualizados del estado.</param>
    public record UpdateAuditStatusCommand(int Id, AuditStatusWriteDto Status) : IRequest;
}