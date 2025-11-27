using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUpStatuses;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.FollowUpStatuses.Handlers
{
    public class FollowUpStatusQueryHandler : IRequestHandler<GetFollowUpStatusesQuery, IEnumerable<FollowUpStatusReadDto>>, IRequestHandler<GetFollowUpStatusByIdQuery, FollowUpStatusReadDto?>
    {
        private readonly IFollowUpStatusReadRepository _readRepo;

        public FollowUpStatusQueryHandler(IFollowUpStatusReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<FollowUpStatusReadDto>> Handle(GetFollowUpStatusesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<FollowUpStatusReadDto>();
            foreach (var s in list)
                dto.Add(new FollowUpStatusReadDto(s.Id, s.Description));
            return dto;
        }

        public async Task<FollowUpStatusReadDto?> Handle(GetFollowUpStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var s = await _readRepo.GetByIdAsync(request.Id);
            if (s == null) return null;
            return new FollowUpStatusReadDto(s.Id, s.Description);
        }
    }
}
