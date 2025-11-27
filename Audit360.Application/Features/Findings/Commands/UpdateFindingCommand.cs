using MediatR;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.Application.Features.Findings.Commands
{
    /// <summary>
    /// Comando para actualizar un hallazgo.
    /// </summary>
    /// <param name="Id">Identificador del hallazgo a actualizar.</param>
    /// <param name="Finding">Datos actualizados del hallazgo.</param>
    public record UpdateFindingCommand(int Id, FindingWriteDto Finding) : IRequest;
}