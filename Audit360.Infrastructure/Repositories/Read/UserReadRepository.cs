using Audit360.Application.Interfaces.Repositories;
using Audit360.Domain.Entities;
using Audit360.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Repositories.Read
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly Audit360DbContext _db;
        public UserReadRepository(Audit360DbContext db) => _db = db;

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await _db.Users.FromSqlRaw("EXEC usp_User_GetList").ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var p = new SqlParameter("@Id", id);
            var list = await _db.Users.FromSqlRaw("EXEC usp_User_GetById @Id", p).ToListAsync();
            return list.FirstOrDefault();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var p = new SqlParameter("@Username", username);
            var list = await _db.Users.FromSqlRaw("EXEC usp_User_GetByUsername @Username", p).ToListAsync();
            return list.FirstOrDefault();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
