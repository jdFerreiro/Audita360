using MediatR;
using Audit360.Application.Features.Dto.Roles;

namespace Audit360.Application.Features.Roles.Queries
{
    public record GetRoleByIdQuery(int Id) : IRequest<RoleReadDto?>;
}