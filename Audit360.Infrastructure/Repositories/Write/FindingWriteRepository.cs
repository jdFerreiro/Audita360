using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class FindingWriteRepository : IFindingWriteRepository
    {
        private readonly Audit360DbContext _db;
        public FindingWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(Finding entity)
        {
            var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@TypeId", entity.Type?.Id ?? 0),
                new SqlParameter("@SeverityId", entity.Severity?.Id ?? 0),
                new SqlParameter("@Date", entity.Date),
                new SqlParameter("@AuditId", entity.AuditId),
                newIdParam
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Finding_Create @Description, @TypeId, @SeverityId, @Date, @AuditId, @NewId OUTPUT", parameters);

            if (newIdParam.Value != DBNull.Value && newIdParam.Value != null)
            {
                entity.Id = Convert.ToInt32(newIdParam.Value);
            }
        }

        public async Task UpdateAsync(Finding entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@TypeId", entity.Type?.Id ?? 0),
                new SqlParameter("@SeverityId", entity.Severity?.Id ?? 0),
                new SqlParameter("@Date", entity.Date),
                new SqlParameter("@AuditId", entity.AuditId)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Finding_Update @Id, @Description, @TypeId, @SeverityId, @Date, @AuditId", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Finding_Delete @Id", p);
        }
    }
}
