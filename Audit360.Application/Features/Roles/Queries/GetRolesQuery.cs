using MediatR;
using System.Collections.Generic;
using Audit360.Application.Features.Dto.Roles;

namespace Audit360.Application.Features.Roles.Queries
{
    public record GetRolesQuery : IRequest<IEnumerable<RoleReadDto>>;
}