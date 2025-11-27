using MediatR;
using Audit360.Application.Features.FindingSeverities.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FindingSeverities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.FindingSeverities.Handlers
{
    public class FindingSeverityQueryHandler : IRequestHandler<GetFindingSeveritiesQuery, IEnumerable<FindingSeverityReadDto>>, IRequestHandler<GetFindingSeverityByIdQuery, FindingSeverityReadDto?>
    {
        private readonly IFindingSeverityReadRepository _readRepo;
        private readonly IMapper _mapper;

        public FindingSeverityQueryHandler(IFindingSeverityReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<FindingSeverityReadDto>> Handle(GetFindingSeveritiesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<FindingSeverityReadDto>>(list);
        }

        public async Task<FindingSeverityReadDto?> Handle(GetFindingSeverityByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            return f == null ? null : _mapper.Map<FindingSeverityReadDto>(f);
        }
    }
}
