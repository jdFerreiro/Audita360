using MediatR;
using Audit360.Application.Features.Users.Queries;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Users;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Audit360.Application.Features.Users.Handlers
{
    public class UserQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserReadDto>>, IRequestHandler<GetUserByIdQuery, UserReadDto?>
    {
        private readonly IUserReadRepository _readRepo;

        public UserQueryHandler(IUserReadRepository readRepo) => _readRepo = readRepo;

        public async Task<IEnumerable<UserReadDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _readRepo.GetListAsync();
            var dtoList = new List<UserReadDto>();
            foreach (var u in list)
            {
                dtoList.Add(new UserReadDto(u.Id, u.Username, u.Email, u.FullName, u.IsActive, u.RoleId, u.CreatedAt));
            }
            return dtoList;
        }

        public async Task<UserReadDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var u = await _readRepo.GetByIdAsync(request.Id);
            if (u == null) return null;
            return new UserReadDto(u.Id, u.Username, u.Email, u.FullName, u.IsActive, u.RoleId, u.CreatedAt);
        }
    }
}
