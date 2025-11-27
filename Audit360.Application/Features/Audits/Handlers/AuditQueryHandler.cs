using MediatR;
using Audit360.Application.Features.Audits.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Audits;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Audits.Handlers
{
    public class AuditQueryHandler : IRequestHandler<GetAuditsQuery, IEnumerable<AuditReadDto>>, IRequestHandler<GetAuditByIdQuery, AuditReadDto?>
    {
        private readonly IAuditReadRepository _readRepo;

        public AuditQueryHandler(IAuditReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<AuditReadDto>> Handle(GetAuditsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dtoList = new List<AuditReadDto>();
            foreach (var a in list)
            {
                dtoList.Add(new AuditReadDto(a.Id, a.Title, a.Area, a.StartDate, a.EndDate, a.Status.Id, a.ResponsibleId));
            }
            return dtoList;
        }

        public async Task<AuditReadDto?> Handle(GetAuditByIdQuery request, CancellationToken cancellationToken)
        {
            var a = await _readRepo.GetByIdAsync(request.Id);
            if (a == null) return null;
            return new AuditReadDto(a.Id, a.Title, a.Area, a.StartDate, a.EndDate, a.Status.Id, a.ResponsibleId);
        }
    }
}
