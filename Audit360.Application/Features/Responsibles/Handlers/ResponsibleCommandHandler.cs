using MediatR;
using Audit360.Application.Features.Responsibles.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Responsibles;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Responsibles.Handlers
{
    public class ResponsibleCommandHandler : IRequestHandler<CreateResponsibleCommand>, IRequestHandler<UpdateResponsibleCommand>, IRequestHandler<DeleteResponsibleCommand>
    {
        private readonly IResponsibleWriteRepository _writeRepo;

        public ResponsibleCommandHandler(IResponsibleWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateResponsibleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Responsible;
            var e = new Responsible { Name = dto.Name, Email = dto.Email, Area = dto.Area };
            await _writeRepo.CreateAsync(e);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateResponsibleCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Responsible;
            var e = new Responsible { Id = request.Id, Name = dto.Name, Email = dto.Email, Area = dto.Area };
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
