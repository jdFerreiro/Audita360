using MediatR;
using Audit360.Application.Features.FollowUps.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUps;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.FollowUps.Handlers
{
    public class FollowUpCommandHandler : IRequestHandler<CreateFollowUpCommand, Unit>, IRequestHandler<UpdateFollowUpCommand, Unit>, IRequestHandler<DeleteFollowUpCommand, Unit>
    {
        private readonly IFollowUpWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public FollowUpCommandHandler(IFollowUpWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateFollowUpCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FollowUp>(request.FollowUp);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFollowUpCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FollowUp>(request.FollowUp);
            e.Id = request.Id;
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
