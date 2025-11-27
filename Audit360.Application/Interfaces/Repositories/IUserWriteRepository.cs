using Audit360.Domain.Entities;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IUserWriteRepository : IWriteRepository<User>
    {
        // Additional write-specific methods for User can be added here
    }
}
