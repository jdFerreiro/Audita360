using MediatR;
using Audit360.Application.Features.Responsibles.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Responsibles;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Responsibles.Handlers
{
    public class ResponsibleQueryHandler : IRequestHandler<GetResponsiblesQuery, IEnumerable<ResponsibleReadDto>>, IRequestHandler<GetResponsibleByIdQuery, ResponsibleReadDto?>
    {
        private readonly IResponsibleReadRepository _readRepo;
        private readonly IMapper _mapper;

        public ResponsibleQueryHandler(IResponsibleReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<ResponsibleReadDto>> Handle(GetResponsiblesQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<ResponsibleReadDto>>(list);
        }

        public async Task<ResponsibleReadDto?> Handle(GetResponsibleByIdQuery request, CancellationToken cancellationToken)
        {
            var r = await _readRepo.GetByIdAsync(request.Id);
            return r == null ? null : _mapper.Map<ResponsibleReadDto>(r);
        }
    }
}
