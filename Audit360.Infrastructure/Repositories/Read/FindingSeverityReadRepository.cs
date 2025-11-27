using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class FindingSeverityReadRepository : IFindingSeverityReadRepository
    {
        private readonly Audit360DbContext _db;
        public FindingSeverityReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<FindingSeverity>> GetListAsync()
        {
            return await _db.FindingSeverities.FromSqlRaw("EXEC usp_FindingSeverity_GetList").ToListAsync();
        }

        public async Task<FindingSeverity?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.FindingSeverities.FromSqlRaw("EXEC usp_FindingSeverity_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
