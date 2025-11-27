using MediatR;
using Audit360.Application.Features.Roles.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Roles;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Roles.Handlers
{
    public class RoleQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleReadDto>>, IRequestHandler<GetRoleByIdQuery, RoleReadDto?>
    {
        private readonly IRoleReadRepository _readRepo;
        private readonly IMapper _mapper;

        public RoleQueryHandler(IRoleReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<RoleReadDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<RoleReadDto>>(list);
        }

        public async Task<RoleReadDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var r = await _readRepo.GetByIdAsync(request.Id);
            return r == null ? null : _mapper.Map<RoleReadDto>(r);
        }
    }
}
