using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class AuditReadRepository : IAuditReadRepository
    {
        private readonly Audit360DbContext _db;
        public AuditReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<Audit>> GetListAsync()
        {
            return await _db.Audits.FromSqlRaw("EXEC usp_Audit_GetList").ToListAsync();
        }

        public async Task<Audit?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.Audits.FromSqlRaw("EXEC usp_Audit_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
