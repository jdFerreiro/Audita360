using MediatR;
using Audit360.Application.Features.Dto.FollowUps;

namespace Audit360.Application.Features.FollowUps.Queries
{
    public record GetFollowUpByIdQuery(int Id) : IRequest<FollowUpReadDto?>;
}