using Dapper;
using NowSoft.Domain.Models;
using NowSoft.Domain.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserSessionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> CreateSessionAsync(UserSession session)
        {
            var sql = @"INSERT INTO UserSessions (UserId, Token, LoginTime, IpAddress, Device, Browser) 
                        VALUES (@UserId, @Token, @LoginTime, @IpAddress, @Device, @Browser);
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            var sessionId = await _dbConnection.QuerySingleAsync<int>(sql, session);
            return sessionId;
        }

        public async Task<UserSession> GetSessionByTokenAsync(string token)
        {
            var sql = "SELECT * FROM UserSessions WHERE Token = @Token";
            var userSession = await _dbConnection.QuerySingleOrDefaultAsync<UserSession>(sql, new { Token = token });
            return userSession;
        }

        public async Task<int> GetLoginCountByUserIdAsync(int userId)
        {
            var query = "SELECT COUNT(*) FROM UserSessions WHERE UserId = @UserId";
            var count = await _dbConnection.ExecuteScalarAsync<int>(query, new { UserId = userId });
            return count;
        }
    }
}
