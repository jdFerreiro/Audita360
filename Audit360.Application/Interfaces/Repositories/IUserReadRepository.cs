using Audit360.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
        // Additional read-specific methods for User can be added here
        Task<User?> GetByUsernameAsync(string username);

        // Added: obtain user by email (needed for authentication)
        Task<User?> GetByEmailAsync(string email);
    }
}
