using MediatR;
using Audit360.Application.Features.FollowUps.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUps;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.FollowUps.Handlers
{
    public class FollowUpCommandHandler : IRequestHandler<CreateFollowUpCommand>, IRequestHandler<UpdateFollowUpCommand>, IRequestHandler<DeleteFollowUpCommand>
    {
        private readonly IFollowUpWriteRepository _writeRepo;

        public FollowUpCommandHandler(IFollowUpWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateFollowUpCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FollowUp;
            var e = new FollowUp
            {
                Description = dto.Description,
                CommitmentDate = dto.CommitmentDate,
                Status = new FollowUpStatus { Id = dto.StatusId, Description = string.Empty },
                FindingId = dto.FindingId
            };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFollowUpCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FollowUp;
            var e = new FollowUp
            {
                Id = request.Id,
                Description = dto.Description,
                CommitmentDate = dto.CommitmentDate,
                Status = new FollowUpStatus { Id = dto.StatusId, Description = string.Empty },
                FindingId = dto.FindingId
            };
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFollowUpCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
