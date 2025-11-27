using MediatR;
using Audit360.Application.Features.FindingTypes.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingTypes;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.FindingTypes.Handlers
{
    public class FindingTypeCommandHandler : IRequestHandler<CreateFindingTypeCommand>, IRequestHandler<UpdateFindingTypeCommand>, IRequestHandler<DeleteFindingTypeCommand>
    {
        private readonly IFindingTypeWriteRepository _writeRepo;

        public FindingTypeCommandHandler(IFindingTypeWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateFindingTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FindingType;
            var e = new FindingType { Description = dto.Description };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateFindingTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.FindingType;
            var e = new FindingType { Id = request.Id, Description = dto.Description };
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
