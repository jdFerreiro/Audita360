using MediatR;
using Audit360.Application.Features.Roles.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Roles;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Roles.Handlers
{
    public class RoleQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleReadDto>>, IRequestHandler<GetRoleByIdQuery, RoleReadDto?>
    {
        private readonly IRoleReadRepository _readRepo;

        public RoleQueryHandler(IRoleReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<RoleReadDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<RoleReadDto>();
            foreach (var r in list)
                dto.Add(new RoleReadDto(r.Id, r.Name, r.Description));
            return dto;
        }

        public async Task<RoleReadDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var r = await _readRepo.GetByIdAsync(request.Id);
            if (r == null) return null;
            return new RoleReadDto(r.Id, r.Name, r.Description);
        }
    }
}
