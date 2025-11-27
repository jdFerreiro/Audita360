using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Queries
{
    public record GetUsersQuery : IRequest<IEnumerable<UserReadDto>>;
}