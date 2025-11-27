using MediatR;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.Application.Features.FollowUps.Commands
{
    /// <summary>
    /// Comando para crear un seguimiento.
    /// </summary>
    /// <param name="FollowUp">Datos del seguimiento a crear.</param>
    public record CreateFollowUpCommand(FollowUpWriteDto FollowUp) : IRequest;
}