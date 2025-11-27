using MediatR;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Commands
{
    /// <summary>
    /// Comando para actualizar un usuario.
    /// </summary>
    /// <param name="Id">Identificador del usuario a actualizar.</param>
    /// <param name="User">Datos actualizados del usuario.</param>
    public record UpdateUserCommand(int Id, UserWriteDto User) : IRequest<MediatR.Unit>;
}