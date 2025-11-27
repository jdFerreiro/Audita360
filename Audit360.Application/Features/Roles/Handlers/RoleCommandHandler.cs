using MediatR;
using Audit360.Application.Features.Roles.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Roles;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.Roles.Handlers
{
    public class RoleCommandHandler : IRequestHandler<CreateRoleCommand>, IRequestHandler<UpdateRoleCommand>, IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public RoleCommandHandler(IRoleWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Role>(request.Role);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Role>(request.Role);
            e.Id = request.Id;
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
