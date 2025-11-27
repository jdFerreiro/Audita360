using MediatR;
using Audit360.Application.Features.Users.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Users;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using AutoMapper;

namespace Audit360.Application.Features.Users.Handlers
{
    public class UserQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserReadDto>>, IRequestHandler<GetUserByIdQuery, UserReadDto?>
    {
        private readonly IUserReadRepository _readRepo;
        private readonly IMapper _mapper;

        public UserQueryHandler(IUserReadRepository readRepo, IMapper mapper) => (_readRepo, _mapper) = (readRepo, mapper);

        public async Task<IEnumerable<UserReadDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(list);
        }

        public async Task<UserReadDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var u = await _readRepo.GetByIdAsync(request.Id);
            return u == null ? null : _mapper.Map<UserReadDto>(u);
        }
    }
}
