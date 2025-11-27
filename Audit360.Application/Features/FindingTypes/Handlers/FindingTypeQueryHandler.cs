using MediatR;
using Audit360.Application.Features.FindingTypes.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingTypes;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.FindingTypes.Handlers
{
    public class FindingTypeQueryHandler : IRequestHandler<GetFindingTypesQuery, IEnumerable<FindingTypeReadDto>>, IRequestHandler<GetFindingTypeByIdQuery, FindingTypeReadDto?>
    {
        private readonly IFindingTypeReadRepository _readRepo;
        private readonly IMapper _mapper;

        public FindingTypeQueryHandler(IFindingTypeReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<FindingTypeReadDto>> Handle(GetFindingTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<FindingTypeReadDto>>(list);
        }

        public async Task<FindingTypeReadDto?> Handle(GetFindingTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            return f == null ? null : _mapper.Map<FindingTypeReadDto>(f);
        }
    }
}
