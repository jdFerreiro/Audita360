using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class RoleReadRepository : IRoleReadRepository
    {
        private readonly Audit360DbContext _db;
        public RoleReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<Role>> GetListAsync()
        {
            return await _db.Roles.FromSqlRaw("EXEC usp_Role_GetList").ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.Roles.FromSqlRaw("EXEC usp_Role_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
