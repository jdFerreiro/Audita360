using MediatR;

namespace Audit360.Application.Features.FollowUps.Commands
{
    public record DeleteFollowUpCommand(int Id) : IRequest;
}