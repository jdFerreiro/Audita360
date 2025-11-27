using MediatR;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.Application.Features.FollowUps.Commands
{
    public record UpdateFollowUpCommand(int Id, FollowUpWriteDto FollowUp) : IRequest;
}