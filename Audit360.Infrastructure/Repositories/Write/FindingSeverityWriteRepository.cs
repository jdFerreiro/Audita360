using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class FindingSeverityWriteRepository : IFindingSeverityWriteRepository
    {
        private readonly Audit360DbContext _db;
        public FindingSeverityWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(FindingSeverity entity)
        {
            var p = new SqlParameter("@Description", entity.Description);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingSeverity_Create @Description", p);
        }

        public async Task UpdateAsync(FindingSeverity entity)
        {
            var parameters = new[] { new SqlParameter("@Id", entity.Id), new SqlParameter("@Description", entity.Description) };
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingSeverity_Update @Id, @Description", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingSeverity_Delete @Id", p);
        }
    }
}
