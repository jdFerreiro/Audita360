using MediatR;
using Audit360.Application.Features.Findings.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Findings;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.Findings.Handlers
{
    public class FindingCommandHandler : IRequestHandler<CreateFindingCommand>, IRequestHandler<UpdateFindingCommand>, IRequestHandler<DeleteFindingCommand>
    {
        private readonly IFindingWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public FindingCommandHandler(IFindingWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateFindingCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Finding>(request.Finding);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Finding>(request.Finding);
            e.Id = request.Id;
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
