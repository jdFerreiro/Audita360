using Microsoft.EntityFrameworkCore;
using Audit360.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly Audit360DbContext _db;
        protected readonly DbSet<T> _set;

        protected RepositoryBase(Audit360DbContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetListAsync()
        {
            return await _set.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            _set.Add(entity);
            await _db.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _set.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
