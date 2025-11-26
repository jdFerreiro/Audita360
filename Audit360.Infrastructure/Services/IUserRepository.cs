using Audit360.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Infrastructure.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetListAsync();
        Task<User?> GetByIdAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
