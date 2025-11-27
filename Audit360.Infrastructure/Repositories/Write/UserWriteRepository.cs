using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Audit360.Infrastructure.Repositories.Write
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly Audit360DbContext _db;
        public UserWriteRepository(Audit360DbContext db) => _db = db;

        public async Task CreateAsync(User entity)
        {
            var newIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
                new SqlParameter("@Username", entity.Username),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@PasswordHash", entity.PasswordHash ?? string.Empty),
                new SqlParameter("@FullName", entity.FullName),
                new SqlParameter("@IsActive", entity.IsActive),
                new SqlParameter("@RoleId", entity.RoleId),
                newIdParam
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_User_Create @Username, @Email, @PasswordHash, @FullName, @IsActive, @RoleId, @NewId OUTPUT", parameters);

            if (newIdParam.Value != DBNull.Value && newIdParam.Value != null)
            {
                entity.Id = Convert.ToInt32(newIdParam.Value);
            }
        }

        public async Task UpdateAsync(User entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Username", entity.Username),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@FullName", entity.FullName),
                new SqlParameter("@IsActive", entity.IsActive),
                new SqlParameter("@RoleId", entity.RoleId)
            };

            await _db.Database.ExecuteSqlRawAsync("EXEC usp_User_Update @Id, @Username, @Email, @FullName, @IsActive, @RoleId", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            await _db.Database.ExecuteSqlRawAsync("EXEC usp_User_Delete @Id", p);
        }
    }
}
