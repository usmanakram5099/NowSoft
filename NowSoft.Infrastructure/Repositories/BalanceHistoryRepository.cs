using Dapper;
using NowSoft.Domain.Models;
using NowSoft.Domain.Interfaces;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Repositories
{
    public class BalanceHistoryRepository : IBalanceHistoryRepository
    {
        private readonly IDbConnection _dbConnection;

        public BalanceHistoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<BalanceHistory>> GetAllAsync()
        {
            var sql = "SELECT * FROM BalanceHistories";
            return await _dbConnection.QueryAsync<BalanceHistory>(sql);
        }

        public async Task<BalanceHistory> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM BalanceHistories WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<BalanceHistory>(sql, new { Id = id });
        }

        public async Task<bool> AddAsync(BalanceHistory balanceHistory)
        {
            var sql = @"INSERT INTO BalanceHistories (UserId, Amount, Date, Description) 
                        VALUES (@UserId, @Amount, @Date, @Description)";
            var result = await _dbConnection.ExecuteAsync(sql, balanceHistory);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(BalanceHistory balanceHistory)
        {
            var sql = @"UPDATE BalanceHistories 
                        SET UserId = @UserId, Amount = @Amount, Date = @Date, Description = @Description 
                        WHERE Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, balanceHistory);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM BalanceHistories WHERE Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
