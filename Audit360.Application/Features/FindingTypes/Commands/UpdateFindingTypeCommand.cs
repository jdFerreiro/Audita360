using MediatR;
using Audit360.Application.Features.Dto.FindingTypes;

namespace Audit360.Application.Features.FindingTypes.Commands
{
    /// <summary>
    /// Comando para actualizar un tipo de hallazgo.
    /// </summary>
    /// <param name="Id">Identificador del tipo a actualizar.</param>
    /// <param name="FindingType">Datos actualizados del tipo.</param>
    public record UpdateFindingTypeCommand(int Id, FindingTypeWriteDto FindingType) : IRequest<MediatR.Unit>;
}