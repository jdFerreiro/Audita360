using MediatR;
using Audit360.Application.Features.FindingTypes.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingTypes;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.FindingTypes.Handlers
{
    public class FindingTypeCommandHandler : IRequestHandler<CreateFindingTypeCommand, Unit>, IRequestHandler<UpdateFindingTypeCommand, Unit>, IRequestHandler<DeleteFindingTypeCommand, Unit>
    {
        private readonly IFindingTypeWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public FindingTypeCommandHandler(IFindingTypeWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateFindingTypeCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FindingType>(request.FindingType);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingTypeCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<FindingType>(request.FindingType);
            e.Id = request.Id;
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteFindingTypeCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
