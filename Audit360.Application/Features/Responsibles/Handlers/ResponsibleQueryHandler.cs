using MediatR;
using Audit360.Application.Features.Responsibles.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Responsibles;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Responsibles.Handlers
{
    public class ResponsibleQueryHandler : IRequestHandler<GetResponsiblesQuery, IEnumerable<ResponsibleReadDto>>, IRequestHandler<GetResponsibleByIdQuery, ResponsibleReadDto?>
    {
        private readonly IResponsibleReadRepository _readRepo;

        public ResponsibleQueryHandler(IResponsibleReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<ResponsibleReadDto>> Handle(GetResponsiblesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<ResponsibleReadDto>();
            foreach (var r in list)
                dto.Add(new ResponsibleReadDto(r.Id, r.Name, r.Email, r.Area));
            return dto;
        }

        public async Task<ResponsibleReadDto?> Handle(GetResponsibleByIdQuery request, CancellationToken cancellationToken)
        {
            var r = await _readRepo.GetByIdAsync(request.Id);
            if (r == null) return null;
            return new ResponsibleReadDto(r.Id, r.Name, r.Email, r.Area);
        }
    }
}
