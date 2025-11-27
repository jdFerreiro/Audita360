using MediatR;
using Audit360.Application.Features.Dto.Roles;

namespace Audit360.Application.Features.Roles.Commands
{
    /// <summary>
    /// Comando para crear un rol.
    /// </summary>
    /// <param name="Role">Datos del rol a crear.</param>
    public record CreateRoleCommand(RoleWriteDto Role) : IRequest<Unit>;
}