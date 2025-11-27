using MediatR;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.Application.Features.FollowUpStatuses.Commands
{
    /// <summary>
    /// Comando para crear un estado de seguimiento.
    /// </summary>
    /// <param name="FollowUpStatus">Datos del estado a crear.</param>
    public record CreateFollowUpStatusCommand(FollowUpStatusWriteDto FollowUpStatus) : IRequest<MediatR.Unit>;
}