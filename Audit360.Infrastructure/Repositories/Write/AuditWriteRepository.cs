using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class AuditWriteRepository : IAuditWriteRepository
    {
        private readonly Audit360DbContext _db;
        public AuditWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(Audit entity)
        {
            var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@Title", entity.Title),
                new SqlParameter("@Area", entity.Area),
                new SqlParameter("@StartDate", entity.StartDate),
                new SqlParameter("@EndDate", (object?)entity.EndDate ?? DBNull.Value),
                new SqlParameter("@StatusId", entity.Status?.Id ?? 0),
                new SqlParameter("@ResponsibleId", entity.ResponsibleId),
                newIdParam
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Audit_Create @Title, @Area, @StartDate, @EndDate, @StatusId, @ResponsibleId, @NewId OUTPUT", parameters);

            // Assign generated id back to entity if stored procedure returned one
            if (newIdParam.Value != DBNull.Value && newIdParam.Value != null)
            {
                entity.Id = Convert.ToInt32(newIdParam.Value);
            }
        }

        public async Task UpdateAsync(Audit entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Title", entity.Title),
                new SqlParameter("@Area", entity.Area),
                new SqlParameter("@StartDate", entity.StartDate),
                new SqlParameter("@EndDate", (object?)entity.EndDate ?? DBNull.Value),
                new SqlParameter("@StatusId", entity.Status?.Id ?? 0),
                new SqlParameter("@ResponsibleId", entity.ResponsibleId)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Audit_Update @Id, @Title, @Area, @StartDate, @EndDate, @StatusId, @ResponsibleId", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Audit_Delete @Id", p);
        }
    }
}
