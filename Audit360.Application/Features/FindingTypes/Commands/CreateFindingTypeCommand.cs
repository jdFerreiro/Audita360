using MediatR;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.Application.Features.FindingTypes.Commands
{
    /// <summary>
    /// Comando para crear un tipo de hallazgo.
    /// </summary>
    /// <param name="FindingType">Datos del tipo a crear.</param>
    public record CreateFindingTypeCommand(FindingTypeWriteDto FindingType) : IRequest<Unit>;
}