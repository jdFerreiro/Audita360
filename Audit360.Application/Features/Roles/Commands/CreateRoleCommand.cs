using MediatR;
using Audit360.Application.Features.Dto.Roles;

namespace Audit360.Application.Features.Roles.Commands
{
    public record CreateRoleCommand(RoleWriteDto Role) : IRequest;
}