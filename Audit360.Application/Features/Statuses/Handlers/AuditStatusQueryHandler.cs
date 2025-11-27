using MediatR;
using Audit360.Application.Features.Statuses.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Statuses;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Statuses.Handlers
{
    public class AuditStatusQueryHandler : IRequestHandler<GetAuditStatusesQuery, IEnumerable<AuditStatusReadDto>>, IRequestHandler<GetAuditStatusByIdQuery, AuditStatusReadDto?>
    {
        private readonly IAuditStatusReadRepository _readRepo;

        public AuditStatusQueryHandler(IAuditStatusReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<AuditStatusReadDto>> Handle(GetAuditStatusesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<AuditStatusReadDto>();
            foreach (var s in list)
                dto.Add(new AuditStatusReadDto(s.Id, s.Description));
            return dto;
        }

        public async Task<AuditStatusReadDto?> Handle(GetAuditStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var s = await _readRepo.GetByIdAsync(request.Id);
            if (s == null) return null;
            return new AuditStatusReadDto(s.Id, s.Description);
        }
    }
}
