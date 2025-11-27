using MediatR;
using Audit360.Application.Features.Findings.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Findings;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Findings.Handlers
{
    public class FindingCommandHandler : IRequestHandler<CreateFindingCommand>, IRequestHandler<UpdateFindingCommand>, IRequestHandler<DeleteFindingCommand>
    {
        private readonly IFindingWriteRepository _writeRepo;

        public FindingCommandHandler(IFindingWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateFindingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Finding;
            var e = new Finding
            {
                Description = dto.Description,
                Type = new FindingType { Id = dto.TypeId, Description = string.Empty },
                Severity = new FindingSeverity { Id = dto.SeverityId, Description = string.Empty },
                Date = dto.Date,
                AuditId = dto.AuditId
            };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Finding;
            var e = new Finding
            {
                Id = request.Id,
                Description = dto.Description,
                Type = new FindingType { Id = dto.TypeId, Description = string.Empty },
                Severity = new FindingSeverity { Id = dto.SeverityId, Description = string.Empty },
                Date = dto.Date,
                AuditId = dto.AuditId
            };
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFindingCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
