using MediatR;
using Audit360.Application.Features.Findings.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Findings;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Findings.Handlers
{
    public class FindingQueryHandler : IRequestHandler<GetFindingsQuery, IEnumerable<FindingReadDto>>, IRequestHandler<GetFindingByIdQuery, FindingReadDto?>
    {
        private readonly IFindingReadRepository _readRepo;
        private readonly IMapper _mapper;

        public FindingQueryHandler(IFindingReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<FindingReadDto>> Handle(GetFindingsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<FindingReadDto>>(list);
        }

        public async Task<FindingReadDto?> Handle(GetFindingByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            return f == null ? null : _mapper.Map<FindingReadDto>(f);
        }
    }
}
