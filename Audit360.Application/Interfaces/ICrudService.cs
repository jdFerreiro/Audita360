using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
