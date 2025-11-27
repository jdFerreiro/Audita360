using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class RoleWriteRepository : IRoleWriteRepository
    {
        private readonly Audit360DbContext _db;
        public RoleWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(Role entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description ?? string.Empty)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Role_Create @Name, @Description", parameters);
        }

        public async Task UpdateAsync(Role entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description ?? string.Empty)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Role_Update @Id, @Name, @Description", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Role_Delete @Id", p);
        }
    }
}
