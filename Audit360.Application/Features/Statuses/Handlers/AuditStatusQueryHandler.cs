using MediatR;
using Audit360.Application.Features.Statuses.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Statuses;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Statuses.Handlers
{
    public class AuditStatusQueryHandler : IRequestHandler<GetAuditStatusesQuery, IEnumerable<AuditStatusReadDto>>, IRequestHandler<GetAuditStatusByIdQuery, AuditStatusReadDto?>
    {
        private readonly IAuditStatusReadRepository _readRepo;
        private readonly IMapper _mapper;

        public AuditStatusQueryHandler(IAuditStatusReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<AuditStatusReadDto>> Handle(GetAuditStatusesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<AuditStatusReadDto>>(list);
        }

        public async Task<AuditStatusReadDto?> Handle(GetAuditStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var s = await _readRepo.GetByIdAsync(request.Id);
            return s == null ? null : _mapper.Map<AuditStatusReadDto>(s);
        }
    }
}
