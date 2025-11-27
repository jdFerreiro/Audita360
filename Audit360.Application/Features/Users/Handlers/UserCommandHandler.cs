using MediatR;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Users;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Audit360.Application.Features.Users.Handlers
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand>, IRequestHandler<UpdateUserCommand>, IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserWriteRepository _writeRepo;

        public UserCommandHandler(IUserWriteRepository writeRepo) => _writeRepo = writeRepo;

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.User;
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password, // assume hashing elsewhere
                FullName = dto.FullName,
                IsActive = dto.IsActive,
                RoleId = dto.RoleId,
                Role = new Role { Id = dto.RoleId, Name = string.Empty }
            };
            await _writeRepo.CreateAsync(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.User;
            var user = new User
            {
                Id = request.Id,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password,
                FullName = dto.FullName,
                IsActive = dto.IsActive,
                RoleId = dto.RoleId,
                Role = new Role { Id = dto.RoleId, Name = string.Empty }
            };
            await _writeRepo.UpdateAsync(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
