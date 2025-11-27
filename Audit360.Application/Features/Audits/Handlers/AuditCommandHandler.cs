using Audit360.Application.Features.Audits.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Audits.Handlers
{
    public class AuditCommandHandler : IRequestHandler<CreateAuditCommand, Unit>, IRequestHandler<UpdateAuditCommand, Unit>, IRequestHandler<DeleteAuditCommand, Unit>
    {
        private readonly IAuditWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public AuditCommandHandler(IAuditWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Audit>(request.Audit);
            await _writeRepo.CreateAsync(entity);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateAuditCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Audit>(request.Audit);
            entity.Id = request.Id;
            await _writeRepo.UpdateAsync(entity);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteAuditCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
