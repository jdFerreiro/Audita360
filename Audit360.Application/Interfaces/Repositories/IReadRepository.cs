using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T?> GetByIdAsync(int id);
    }
}
