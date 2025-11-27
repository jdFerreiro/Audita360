using MediatR;
using Audit360.Application.Features.Dto.FindingSeverities;

namespace Audit360.Application.Features.FindingSeverities.Commands
{
    /// <summary>
    /// Comando para actualizar una severidad de hallazgo.
    /// </summary>
    /// <param name="Id">Identificador de la severidad a actualizar.</param>
    /// <param name="FindingSeverity">Datos actualizados de la severidad.</param>
    public record UpdateFindingSeverityCommand(int Id, FindingSeverityWriteDto FindingSeverity) : IRequest<MediatR.Unit>;
}