using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Users;

namespace Audit360.Application.Features.Users.Queries
{
    /// <summary>
    /// Query para obtener la lista de usuarios.
    /// </summary>
    public record GetUsersQuery : IRequest<IEnumerable<UserReadDto>>;
}