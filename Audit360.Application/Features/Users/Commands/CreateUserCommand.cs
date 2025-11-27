using MediatR;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Commands
{
    /// <summary>
    /// Comando para crear un usuario.
    /// </summary>
    /// <param name="User">Datos del usuario a crear.</param>
    public record CreateUserCommand(UserWriteDto User) : IRequest<MediatR.Unit>;
}