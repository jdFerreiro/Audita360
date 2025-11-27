using MediatR;
using Audit360.Application.Features.FindingSeverities.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingSeverities;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.FindingSeverities.Handlers
{
    public class FindingSeverityCommandHandler : IRequestHandler<CreateFindingSeverityCommand>, IRequestHandler<UpdateFindingSeverityCommand>, IRequestHandler<DeleteFindingSeverityCommand>
    {
        private readonly IFindingSeverityWriteRepository _writeRepo;

        public FindingSeverityCommandHandler(IFindingSeverityWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateFindingSeverityCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FindingSeverity;
            var e = new FindingSeverity { Description = dto.Description };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingSeverityCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FindingSeverity;
            var e = new FindingSeverity { Id = request.Id, Description = dto.Description };
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFindingSeverityCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
