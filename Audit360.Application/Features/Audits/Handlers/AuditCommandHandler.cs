using MediatR;
using Audit360.Application.Features.Audits.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Audits;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Audits.Handlers
{
    public class AuditCommandHandler : IRequestHandler<CreateAuditCommand>, IRequestHandler<UpdateAuditCommand>, IRequestHandler<DeleteAuditCommand>
    {
        private readonly IAuditWriteRepository _writeRepo;

        public AuditCommandHandler(IAuditWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Audit;
            var entity = new Audit
            {
                Title = dto.Title,
                Area = dto.Area,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = new AuditStatus { Id = dto.StatusId, Description = string.Empty },
                ResponsibleId = dto.ResponsibleId
            };
            await _writeRepo.CreateAsync(entity);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateAuditCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Audit;
            var entity = new Audit
            {
                Id = request.Id,
                Title = dto.Title,
                Area = dto.Area,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = new AuditStatus { Id = dto.StatusId, Description = string.Empty },
                ResponsibleId = dto.ResponsibleId
            };
            await _writeRepo.UpdateAsync(entity);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAuditCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
