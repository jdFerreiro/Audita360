using MediatR;

namespace Audit360.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : IRequest;
}