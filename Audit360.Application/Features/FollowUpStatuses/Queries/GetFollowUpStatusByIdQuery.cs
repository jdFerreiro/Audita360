using MediatR;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.Application.Features.FollowUpStatuses.Queries
{
    public record GetFollowUpStatusByIdQuery(int Id) : IRequest<FollowUpStatusReadDto?>;
}