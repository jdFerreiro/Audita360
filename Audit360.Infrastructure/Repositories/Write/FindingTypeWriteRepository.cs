using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class FindingTypeWriteRepository : IFindingTypeWriteRepository
    {
        private readonly Audit360DbContext _db;
        public FindingTypeWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(FindingType entity)
        {
            var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[] { new SqlParameter("@Description", entity.Description), newIdParam };
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingType_Create @Description, @NewId OUTPUT", parameters);

            if (newIdParam.Value != DBNull.Value && newIdParam.Value != null)
            {
                entity.Id = Convert.ToInt32(newIdParam.Value);
            }
        }

        public async Task UpdateAsync(FindingType entity)
        {
            var parameters = new[] { new SqlParameter("@Id", entity.Id), new SqlParameter("@Description", entity.Description) };
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingType_Update @Id, @Description", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_FindingType_Delete @Id", p);
        }
    }
}
