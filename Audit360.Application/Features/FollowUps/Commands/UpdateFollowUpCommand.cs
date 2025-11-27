using MediatR;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.Application.Features.FollowUps.Commands
{
    /// <summary>
    /// Comando para actualizar un seguimiento.
    /// </summary>
    /// <param name="Id">Identificador del seguimiento a actualizar.</param>
    /// <param name="FollowUp">Datos actualizados del seguimiento.</param>
    public record UpdateFollowUpCommand(int Id, FollowUpWriteDto FollowUp) : IRequest<MediatR.Unit>;
}