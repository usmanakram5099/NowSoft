using NowSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Domain.Interfaces
{
    public interface IBalanceHistoryRepository
    {
        Task<IEnumerable<BalanceHistory>> GetAllAsync();
        Task<BalanceHistory> GetByIdAsync(int id);
        Task<bool> AddAsync(BalanceHistory balanceHistory);
        Task<bool> UpdateAsync(BalanceHistory balanceHistory);
        Task<bool> DeleteAsync(int id);
    }
}
