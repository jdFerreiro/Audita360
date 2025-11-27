using MediatR;
using Audit360.Application.Features.FindingSeverities.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingSeverities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.FindingSeverities.Handlers
{
    public class FindingSeverityQueryHandler : IRequestHandler<GetFindingSeveritiesQuery, IEnumerable<FindingSeverityReadDto>>, IRequestHandler<GetFindingSeverityByIdQuery, FindingSeverityReadDto?>
    {
        private readonly IFindingSeverityReadRepository _readRepo;

        public FindingSeverityQueryHandler(IFindingSeverityReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<FindingSeverityReadDto>> Handle(GetFindingSeveritiesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<FindingSeverityReadDto>();
            foreach (var f in list)
                dto.Add(new FindingSeverityReadDto(f.Id, f.Description));
            return dto;
        }

        public async Task<FindingSeverityReadDto?> Handle(GetFindingSeverityByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            if (f == null) return null;
            return new FindingSeverityReadDto(f.Id, f.Description);
        }
    }
}
