using Audit360.Domain.Entities;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces
{
    public interface IUserService : ICrudService<User>
    {
        // Additional user-specific methods can be added here
    }
}
