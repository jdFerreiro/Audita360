using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.FollowUpStatuses;

namespace Audit360.Application.Features.FollowUpStatuses.Queries
{
    public record GetFollowUpStatusesQuery : IRequest<IEnumerable<FollowUpStatusReadDto>>;
}