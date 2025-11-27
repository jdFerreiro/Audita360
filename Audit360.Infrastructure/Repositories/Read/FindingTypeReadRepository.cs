using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class FindingTypeReadRepository : IFindingTypeReadRepository
    {
        private readonly Audit360DbContext _db;
        public FindingTypeReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<FindingType>> GetListAsync()
        {
            return await _db.FindingTypes.FromSqlRaw("EXEC usp_FindingType_GetList").ToListAsync();
        }

        public async Task<FindingType?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.FindingTypes.FromSqlRaw("EXEC usp_FindingType_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
