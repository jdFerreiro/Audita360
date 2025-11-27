using MediatR;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.Application.Features.FollowUpStatuses.Commands
{
    public record UpdateFollowUpStatusCommand(int Id, FollowUpStatusWriteDto FollowUpStatus) : IRequest;
}