using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowSoft.Application.Dtos;

namespace NowSoft.Application.Interfaces
{
    public interface IBalanceHistoryService
    {
        Task<IEnumerable<BalanceHistoryDto>> GetAllBalanceHistoryAsync();
        Task<BalanceHistoryDto> GetBalanceHistoryByIdAsync(int id);
        Task<bool> AddBalanceHistoryAsync(BalanceHistoryCreateDto balanceHistoryCreateDto);
    }
}
