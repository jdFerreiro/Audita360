using MediatR;
using Audit360.Application.Features.FollowUps.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUps;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.FollowUps.Handlers
{
    public class FollowUpQueryHandler : IRequestHandler<GetFollowUpsQuery, IEnumerable<FollowUpReadDto>>, IRequestHandler<GetFollowUpByIdQuery, FollowUpReadDto?>
    {
        private readonly IFollowUpReadRepository _readRepo;

        public FollowUpQueryHandler(IFollowUpReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<FollowUpReadDto>> Handle(GetFollowUpsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dto = new List<FollowUpReadDto>();
            foreach (var f in list)
                dto.Add(new FollowUpReadDto(f.Id, f.Description, f.CommitmentDate, f.Status.Id, f.FindingId));
            return dto;
        }

        public async Task<FollowUpReadDto?> Handle(GetFollowUpByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            if (f == null) return null;
            return new FollowUpReadDto(f.Id, f.Description, f.CommitmentDate, f.Status.Id, f.FindingId);
        }
    }
}
