using MediatR;
using Audit360.Application.Features.FollowUps.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.FollowUps;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.FollowUps.Handlers
{
    public class FollowUpQueryHandler : IRequestHandler<GetFollowUpsQuery, IEnumerable<FollowUpReadDto>>, IRequestHandler<GetFollowUpByIdQuery, FollowUpReadDto?>
    {
        private readonly IFollowUpReadRepository _readRepo;
        private readonly IMapper _mapper;

        public FollowUpQueryHandler(IFollowUpReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<FollowUpReadDto>> Handle(GetFollowUpsQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<FollowUpReadDto>>(list);
        }

        public async Task<FollowUpReadDto?> Handle(GetFollowUpByIdQuery request, CancellationToken cancellationToken)
        {
            var f = await _readRepo.GetByIdAsync(request.Id);
            return f == null ? null : _mapper.Map<FollowUpReadDto>(f);
        }
    }
}
