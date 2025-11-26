using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Audit360.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly Audit360DbContext _db;

        public UserRepository(Audit360DbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            var result = new List<User>();
            var conn = _db.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_User_GetList";
            cmd.CommandType = CommandType.StoredProcedure;

            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(Map(reader));
            }

            return result;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var conn = _db.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_User_GetById";
            cmd.CommandType = CommandType.StoredProcedure;
            var p = cmd.CreateParameter();
            p.ParameterName = "@Id";
            p.DbType = DbType.Int32;
            p.Value = id;
            cmd.Parameters.Add(p);

            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return Map(reader);

            return null;
        }

        public async Task CreateAsync(User user)
        {
            var conn = _db.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_User_Create";
            cmd.CommandType = CommandType.StoredProcedure;

            AddParam(cmd, "@Username", user.Username);
            AddParam(cmd, "@Email", user.Email);
            AddParam(cmd, "@PasswordHash", user.PasswordHash);
            AddParam(cmd, "@FullName", user.FullName);
            AddParam(cmd, "@IsActive", user.IsActive);
            AddParam(cmd, "@RoleId", user.RoleId);

            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var conn = _db.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_User_Update";
            cmd.CommandType = CommandType.StoredProcedure;

            AddParam(cmd, "@Id", user.Id);
            AddParam(cmd, "@Username", user.Username);
            AddParam(cmd, "@Email", user.Email);
            AddParam(cmd, "@FullName", user.FullName);
            AddParam(cmd, "@IsActive", user.IsActive);
            AddParam(cmd, "@RoleId", user.RoleId);

            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var conn = _db.Database.GetDbConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_User_Delete";
            cmd.CommandType = CommandType.StoredProcedure;
            AddParam(cmd, "@Id", id);

            if (conn.State == ConnectionState.Closed)
                await conn.OpenAsync();

            await cmd.ExecuteNonQueryAsync();
        }

        private static void AddParam(DbCommand cmd, string name, object? value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(p);
        }

        private static User Map(DbDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }
    }
}
