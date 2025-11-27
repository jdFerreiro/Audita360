using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Audits;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Audits.Handlers
{
    public class AuditQueryHandler : IRequestHandler<GetAuditsQuery, IEnumerable<AuditReadDto>>, IRequestHandler<GetAuditByIdQuery, AuditReadDto?>
    {
        private readonly IAuditReadRepository _readRepo;
        private readonly IMapper _mapper;

        public AuditQueryHandler(IAuditReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<AuditReadDto>> Handle(GetAuditsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<AuditReadDto>>(list);
        }

        public async Task<AuditReadDto?> Handle(GetAuditByIdQuery request, CancellationToken cancellationToken)
        {
            var a = await _readRepo.GetByIdAsync(request.Id);
            return a == null ? null : _mapper.Map<AuditReadDto>(a);
        }
    }
}
