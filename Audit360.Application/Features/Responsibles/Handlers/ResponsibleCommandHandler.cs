using MediatR;
using Audit360.Application.Features.Responsibles.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Responsibles;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.Responsibles.Handlers
{
    public class ResponsibleCommandHandler : IRequestHandler<CreateResponsibleCommand, Unit>, IRequestHandler<UpdateResponsibleCommand, Unit>, IRequestHandler<DeleteResponsibleCommand, Unit>
    {
        private readonly IResponsibleWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public ResponsibleCommandHandler(IResponsibleWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateResponsibleCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Responsible>(request.Responsible);
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateResponsibleCommand request, CancellationToken cancellationToken)
        {
            var e = _mapper.Map<Responsible>(request.Responsible);
            e.Id = request.Id;
            await _writeRepo.UpdateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteResponsibleCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
