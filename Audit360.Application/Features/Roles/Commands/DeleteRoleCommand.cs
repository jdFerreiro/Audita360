using MediatR;

namespace Audit360.Application.Features.Roles.Commands
{
    public record DeleteRoleCommand(int Id) : IRequest<MediatR.Unit>;
}