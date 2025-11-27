using MediatR;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Features.Dto.Users;
using Audit360.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Audit360.Application.Features.Users.Handlers
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand>, IRequestHandler<UpdateUserCommand>, IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserWriteRepository _writeRepo;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserWriteRepository writeRepo, IMapper mapper) => (_writeRepo, _mapper) = (writeRepo, mapper);

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            await _writeRepo.CreateAsync(user);
            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            user.Id = request.Id;
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
