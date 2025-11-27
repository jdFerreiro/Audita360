using MediatR;
using Audit360.Application.Features.FindingSeverities.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingSeverities;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.FindingSeverities.Handlers
{
    public class FindingSeverityCommandHandler : IRequestHandler<CreateFindingSeverityCommand>, IRequestHandler<UpdateFindingSeverityCommand>, IRequestHandler<DeleteFindingSeverityCommand>
    {
        private readonly IFindingSeverityWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public FindingSeverityCommandHandler(IFindingSeverityWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateFindingSeverityCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FindingSeverity>(request.FindingSeverity);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingSeverityCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FindingSeverity>(request.FindingSeverity);
            e.Id = request.Id;
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
