using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class AuditFinalizedSummaryReadRepository : IAuditFinalizedSummaryReadRepository
    {
        private readonly Audit360DbContext _db;
        public AuditFinalizedSummaryReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<AuditFinalizedSummary>> GetListAsync()
        {
            return await _db.AuditFinalizedSummaries.ToListAsync();
        }
    }
}
