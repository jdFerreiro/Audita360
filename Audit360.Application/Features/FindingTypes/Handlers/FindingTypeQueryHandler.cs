using MediatR;
using Audit360.Application.Features.FindingTypes.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingTypes;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.FindingTypes.Handlers
{
    public class FindingTypeQueryHandler : IRequestHandler<GetFindingTypesQuery, IEnumerable<FindingTypeReadDto>>, IRequestHandler<GetFindingTypeByIdQuery, FindingTypeReadDto?>
    {
        private readonly IFindingTypeReadRepository _readRepo;

        public FindingTypeQueryHandler(IFindingTypeReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<FindingTypeReadDto>> Handle(GetFindingTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<FindingTypeReadDto>();
            foreach (var f in list)
                dto.Add(new FindingTypeReadDto(f.Id, f.Description));
            return dto;
        }

        public async Task<FindingTypeReadDto?> Handle(GetFindingTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            if (f == null) return null;
            return new FindingTypeReadDto(f.Id, f.Description);
        }
    }
}
