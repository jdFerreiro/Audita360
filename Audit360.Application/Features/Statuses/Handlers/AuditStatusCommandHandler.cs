using MediatR;
using Audit360.Application.Features.Statuses.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Statuses;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Statuses.Handlers
{
    public class AuditStatusCommandHandler : IRequestHandler<CreateAuditStatusCommand>, IRequestHandler<UpdateAuditStatusCommand>, IRequestHandler<DeleteAuditStatusCommand>
    {
        private readonly IAuditStatusWriteRepository _writeRepo;

        public AuditStatusCommandHandler(IAuditStatusWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateAuditStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AuditStatus;
            var e = new AuditStatus { Description = dto.Description };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateAuditStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AuditStatus;
            var e = new AuditStatus { Id = request.Id, Description = dto.Description };
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAuditStatusCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
