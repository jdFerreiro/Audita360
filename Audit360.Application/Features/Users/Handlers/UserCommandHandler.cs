using MediatR;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Users;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using Audit360.Application.Interfaces;

namespace Audit360.Application.Features.Users.Handlers
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand, Unit>, IRequestHandler<UpdateUserCommand, Unit>, IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserWriteRepository _writeRepo;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserCommandHandler(IUserWriteRepository writeRepo, IMapper mapper, IPasswordService passwordService) => (_writeRepo, _mapper, _passwordService) = (writeRepo, mapper, passwordService);

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);

            // Hash password before saving
            if (!string.IsNullOrWhiteSpace(request.User.Password))
            {
                user.PasswordHash = _passwordService.HashPassword(request.User.Password);
            }

            await _writeRepo.CreateAsync(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            user.Id = request.Id;

            // If a password is provided, hash it before update
            if (!string.IsNullOrWhiteSpace(request.User.Password))
            {
                user.PasswordHash = _passwordService.HashPassword(request.User.Password);
            }

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
