using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
