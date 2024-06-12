using NowSoft.Application.Dtos;
using NowSoft.Application.Interfaces;
using NowSoft.Domain.Interfaces;
using NowSoft.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NowSoft.Application.Services
{
    public class BalanceHistoryService : IBalanceHistoryService
    {
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;

        public BalanceHistoryService(IBalanceHistoryRepository balanceHistoryRepository)
        {
            _balanceHistoryRepository = balanceHistoryRepository;
        }

        public async Task<IEnumerable<BalanceHistoryDto>> GetAllBalanceHistoryAsync()
        {
            var balanceHistories = await _balanceHistoryRepository.GetAllAsync();
            return balanceHistories.Select(bh => new BalanceHistoryDto
            {
                Id = bh.Id,
                UserId = bh.UserId,
                Amount = bh.Amount,
                Date = bh.Date,
                Description = bh.Description
            });
        }

        public async Task<BalanceHistoryDto> GetBalanceHistoryByIdAsync(int id)
        {
            var balanceHistory = await _balanceHistoryRepository.GetByIdAsync(id);
            if (balanceHistory == null) return null;

            return new BalanceHistoryDto
            {
                Id = balanceHistory.Id,
                UserId = balanceHistory.UserId,
                Amount = balanceHistory.Amount,
                Date = balanceHistory.Date,
                Description = balanceHistory.Description
            };
        }

        public async Task<bool> AddBalanceHistoryAsync(BalanceHistoryCreateDto balanceHistoryCreateDto)
        {
            var balanceHistory = new BalanceHistory
            {
                UserId = balanceHistoryCreateDto.UserId,
                Amount = balanceHistoryCreateDto.Amount,
                Date = balanceHistoryCreateDto.Date,
                Description = balanceHistoryCreateDto.Description
            };

            return await _balanceHistoryRepository.AddAsync(balanceHistory);
        }
    }
}
