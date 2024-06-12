using Dapper;
using NowSoft.Domain.Models;
using NowSoft.Domain.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            var sql = @"INSERT INTO Users (Username, Password, FirstName, LastName, Device, IpAddress, Balance) 
                        VALUES (@Username, @Password, @FirstName, @LastName, @Device, @IpAddress, @Balance);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            var userId = await _dbConnection.QuerySingleAsync<int>(sql, user);
            return userId;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            var user = await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";
            var result = await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
            return result;
        }
    }
}
