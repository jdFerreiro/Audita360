using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class ResponsibleWriteRepository : IResponsibleWriteRepository
    {
        private readonly Audit360DbContext _db;
        public ResponsibleWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(Responsible entity)
        {
            var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Area", entity.Area),
                newIdParam
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Responsible_Create @Name, @Email, @Area, @NewId OUTPUT", parameters);

            if (newIdParam.Value != DBNull.Value && newIdParam.Value != null)
            {
                entity.Id = Convert.ToInt32(newIdParam.Value);
            }
        }

        public async Task UpdateAsync(Responsible entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Area", entity.Area)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Responsible_Update @Id, @Name, @Email, @Area", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_Responsible_Delete @Id", p);
        }
    }
}
