using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Features.Dto.Audits;
using Audit360.Application.Interfaces.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Audit360.Application.Features.Audits.Handlers
{
    public class AuditFinalizedSummaryQueryHandler : IRequestHandler<GetAuditFinalizedSummaryQuery, IEnumerable<AuditFinalizedSummaryReadDto>>
    {
        private readonly IAuditFinalizedSummaryReadRepository _repo;
        private readonly IMapper _mapper;

        public AuditFinalizedSummaryQueryHandler(IAuditFinalizedSummaryReadRepository repo, IMapper mapper) => (_repo, _mapper) = (repo, mapper);

        public async Task<IEnumerable<AuditFinalizedSummaryReadDto>> Handle(GetAuditFinalizedSummaryQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.GetListAsync();
            return _mapper.Map<IEnumerable<AuditFinalizedSummaryReadDto>>(list);
        }
    }
}
