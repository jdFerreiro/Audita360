using MediatR;

namespace Audit360.Application.Features.FollowUpStatuses.Commands
{
    public record DeleteFollowUpStatusCommand(int Id) : IRequest;
}