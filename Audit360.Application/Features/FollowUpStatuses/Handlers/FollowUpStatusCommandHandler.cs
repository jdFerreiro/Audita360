using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUpStatuses;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.FollowUpStatuses.Handlers
{
    public class FollowUpStatusCommandHandler : IRequestHandler<CreateFollowUpStatusCommand>, IRequestHandler<UpdateFollowUpStatusCommand>, IRequestHandler<DeleteFollowUpStatusCommand>
    {
        private readonly IFollowUpStatusWriteRepository _writeRepo;

        public FollowUpStatusCommandHandler(IFollowUpStatusWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateFollowUpStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FollowUpStatus;
            var e = new FollowUpStatus { Description = dto.Description };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFollowUpStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FollowUpStatus;
            var e = new FollowUpStatus { Id = request.Id, Description = dto.Description };
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFollowUpStatusCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
