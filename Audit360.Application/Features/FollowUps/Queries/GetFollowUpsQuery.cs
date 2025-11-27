using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.Application.Features.FollowUps.Queries
{
    public record GetFollowUpsQuery : IRequest<IEnumerable<FollowUpReadDto>>;
}