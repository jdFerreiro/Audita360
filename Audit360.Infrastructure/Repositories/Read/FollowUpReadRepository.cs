using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class FollowUpReadRepository : IFollowUpReadRepository
    {
        private readonly Audit360DbContext _db;
        public FollowUpReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<FollowUp>> GetListAsync()
        {
            return await _db.FollowUps.FromSqlRaw("EXEC usp_FollowUp_GetList").ToListAsync();
        }

        public async Task<FollowUp?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.FollowUps.FromSqlRaw("EXEC usp_FollowUp_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
