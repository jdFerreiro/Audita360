using MediatR;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserReadDto?>;
}