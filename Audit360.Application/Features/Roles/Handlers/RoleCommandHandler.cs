using MediatR;
using Audit360.Application.Features.Roles.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Roles;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Roles.Handlers
{
    public class RoleCommandHandler : IRequestHandler<CreateRoleCommand>, IRequestHandler<UpdateRoleCommand>, IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleWriteRepository _writeRepo;

        public RoleCommandHandler(IRoleWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Role;
            var e = new Role { Name = dto.Name, Description = dto.Description };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Role;
            var e = new Role { Id = request.Id, Name = dto.Name, Description = dto.Description };
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
