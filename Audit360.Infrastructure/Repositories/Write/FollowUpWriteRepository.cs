using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class FollowUpWriteRepository : IFollowUpWriteRepository
    {
        private readonly Audit360DbContext _db;
        public FollowUpWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(FollowUp entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@CommitmentDate", entity.CommitmentDate),
                new SqlParameter("@StatusId", entity.Status?.Id ?? 0),
                new SqlParameter("@FindingId", entity.FindingId)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FollowUp_Create @Description, @CommitmentDate, @StatusId, @FindingId", parameters);
        }

        public async Task UpdateAsync(FollowUp entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@CommitmentDate", entity.CommitmentDate),
                new SqlParameter("@StatusId", entity.Status?.Id ?? 0),
                new SqlParameter("@FindingId", entity.FindingId)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FollowUp_Update @Id, @Description, @CommitmentDate, @StatusId, @FindingId", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FollowUp_Delete @Id", p);
        }
    }
}
