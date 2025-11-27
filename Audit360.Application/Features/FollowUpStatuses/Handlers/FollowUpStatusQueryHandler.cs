using MediatR;
using Audit360.Application.Features.FollowUpStatuses.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUpStatuses;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.FollowUpStatuses.Handlers
{
    public class FollowUpStatusQueryHandler : IRequestHandler<GetFollowUpStatusesQuery, IEnumerable<FollowUpStatusReadDto>>, IRequestHandler<GetFollowUpStatusByIdQuery, FollowUpStatusReadDto?>
    {
        private readonly IFollowUpStatusReadRepository _readRepo;
        private readonly IMapper _mapper;

        public FollowUpStatusQueryHandler(IFollowUpStatusReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<FollowUpStatusReadDto>> Handle(GetFollowUpStatusesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<FollowUpStatusReadDto>>(list);
        }

        public async Task<FollowUpStatusReadDto?> Handle(GetFollowUpStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var s = await _readRepo.GetByIdAsync(request.Id);
            return s == null ? null : _mapper.Map<FollowUpStatusReadDto>(s);
        }
    }
}
