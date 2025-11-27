using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class AuditStatusReadRepository : IAuditStatusReadRepository
    {
        private readonly Audit360DbContext _db;
        public AuditStatusReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<AuditStatus>> GetListAsync()
        {
            return await _db.AuditStatuses.FromSqlRaw("EXEC usp_AuditStatus_GetList").ToListAsync();
        }

        public async Task<AuditStatus?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.AuditStatuses.FromSqlRaw("EXEC usp_AuditStatus_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
