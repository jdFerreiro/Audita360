using MediatR;
using Audit360.Application.Features.Statuses.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Statuses;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.Statuses.Handlers
{
    public class AuditStatusCommandHandler : IRequestHandler<CreateAuditStatusCommand, Unit>, IRequestHandler<UpdateAuditStatusCommand, Unit>, IRequestHandler<DeleteAuditStatusCommand, Unit>
    {
        private readonly IAuditStatusWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public AuditStatusCommandHandler(IAuditStatusWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateAuditStatusCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<AuditStatus>(request.AuditStatus);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateAuditStatusCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<AuditStatus>(request.AuditStatus);
            e.Id = request.Id;
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAuditStatusCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
