using MediatR;
using Audit360.Application.Features.Findings.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Findings;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Findings.Handlers
{
    public class FindingQueryHandler : IRequestHandler<GetFindingsQuery, IEnumerable<FindingReadDto>>, IRequestHandler<GetFindingByIdQuery, FindingReadDto?>
    {
        private readonly IFindingReadRepository _readRepo;

        public FindingQueryHandler(IFindingReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<FindingReadDto>> Handle(GetFindingsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<FindingReadDto>();
            foreach (var f in list)
                dto.Add(new FindingReadDto(f.Id, f.Description, f.Type.Id, f.Severity.Id, f.Date, f.AuditId));
            return dto;
        }

        public async Task<FindingReadDto?> Handle(GetFindingByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            if (f == null) return null;
            return new FindingReadDto(f.Id, f.Description, f.Type.Id, f.Severity.Id, f.Date, f.AuditId);
        }
    }
}
