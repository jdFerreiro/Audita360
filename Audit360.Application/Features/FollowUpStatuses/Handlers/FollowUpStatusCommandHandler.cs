using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUpStatuses;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.FollowUpStatuses.Handlers
{
    public class FollowUpStatusCommandHandler : IRequestHandler<CreateFollowUpStatusCommand>, IRequestHandler<UpdateFollowUpStatusCommand>, IRequestHandler<DeleteFollowUpStatusCommand>
    {
        private readonly IFollowUpStatusWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public FollowUpStatusCommandHandler(IFollowUpStatusWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateFollowUpStatusCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FollowUpStatus>(request.FollowUpStatus);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFollowUpStatusCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FollowUpStatus>(request.FollowUpStatus);
            e.Id = request.Id;
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
