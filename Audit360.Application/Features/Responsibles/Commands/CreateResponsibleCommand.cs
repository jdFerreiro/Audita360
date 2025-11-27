using MediatR;
using Audit360.Application.Features.Dto.Responsibles;

namespace Audit360.Application.Features.Responsibles.Commands
{
    /// <summary>
    /// Comando para crear un responsable.
    /// </summary>
    /// <param name="Responsible">Datos del responsable a crear.</param>
    public record CreateResponsibleCommand(ResponsibleWriteDto Responsible) : IRequest;
}