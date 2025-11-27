using MediatR;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Commands
{
    public record AuthenticateUserCommand(string Email, string Password) : IRequest<AuthenticateResponseDto>;
}
