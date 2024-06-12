using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowSoft.Application.Dtos;
using NowSoft.Domain.Models;

namespace NowSoft.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<int> GetUserBalanceByIdAsync(int id);
        Task<int> CreateUserAsync(UserCreateDto userCreateDto);
        Task<User> AuthenticateAsync(string username, string password);
        Task<bool> IsFirstLoginAsync(int userId);
        Task AddBalanceAsync(int userId, decimal amount, string description);
    }

}
