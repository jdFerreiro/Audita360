using Audit360.Application.Interfaces;
using Audit360.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(User entity) => _repo.CreateAsync(entity);

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<User?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task<IEnumerable<User>> GetListAsync() => _repo.GetListAsync();

        public Task UpdateAsync(User entity) => _repo.UpdateAsync(entity);
    }
}
