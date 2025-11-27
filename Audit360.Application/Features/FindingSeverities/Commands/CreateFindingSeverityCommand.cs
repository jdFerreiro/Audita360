using MediatR;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.Application.Features.FindingSeverities.Commands
{
    /// <summary>
    /// Comando para crear una severidad de hallazgo.
    /// </summary>
    /// <param name="FindingSeverity">Datos de la severidad a crear.</param>
    public record CreateFindingSeverityCommand(FindingSeverityWriteDto FindingSeverity) : IRequest;
}