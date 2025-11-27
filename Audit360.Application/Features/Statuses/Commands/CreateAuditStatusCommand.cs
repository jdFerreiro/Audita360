using MediatR;
using Audit360.Application.Features.Dto.Statuses;

namespace Audit360.Application.Features.Statuses.Commands
{
    /// <summary>
    /// Comando para crear un estado de auditoría.
    /// </summary>
    /// <param name="Status">Datos del estado a crear.</param>
    public record CreateAuditStatusCommand(AuditStatusWriteDto Status) : IRequest;
}