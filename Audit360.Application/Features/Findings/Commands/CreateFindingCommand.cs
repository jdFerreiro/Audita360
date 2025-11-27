using MediatR;
using Audit360.Application.Features.Dto.Findings;

namespace Audit360.Application.Features.Findings.Commands
{
    /// <summary>
    /// Comando para crear un hallazgo.
    /// </summary>
    /// <param name="Finding">Datos del hallazgo a crear.</param>
    public record CreateFindingCommand(FindingWriteDto Finding) : IRequest;
}