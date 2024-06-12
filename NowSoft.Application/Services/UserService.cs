using NowSoft.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowSoft.Application.Dtos;
using NowSoft.Domain.Interfaces;
using NowSoft.Domain.Models;
using System.Net.Http;

namespace NowSoft.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IBalanceHistoryRepository _balanceHistoryRepository;

        public UserService(IUserRepository userRepository, IUserSessionRepository userSessionRepository, IBalanceHistoryRepository balanceHistoryRepository)
        {
            _userRepository = userRepository;
            _userSessionRepository = userSessionRepository;
            _balanceHistoryRepository = balanceHistoryRepository;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;
            return new UserDto(user);
        }

        public async Task<int> GetUserBalanceByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return 0;
            return user.Balance;
        }

        public async Task<int> CreateUserAsync(UserCreateDto userCreateDto)
        {
            //string clientIpAddress = HttpContext.Current.Request.UserHostAddress;

            var user = new User
            {
                Username = userCreateDto.Username,
                Password = userCreateDto.Password,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Device = "Windows 10",
                IpAddress = "127.0.0.1",
                Balance = 0,
                Browser = "Chrome", 


            };

            return await _userRepository.CreateUserAsync(user);
        }

        // In your UserService class
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            // Logic to validate the username and password
            // For demonstration, let's assume you have a method to check the credentials
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task<bool> IsFirstLoginAsync(int userId)
        {
            // Check if the user has any previous login records
            var isFirstLogin = await _userSessionRepository.GetLoginCountByUserIdAsync(userId) == 0;
            return isFirstLogin;
        }

        public async Task AddBalanceAsync(int userId, decimal amount, string description)
        {
            // Logic to add balance to the user's account
            await _balanceHistoryRepository.AddAsync(new BalanceHistory
            {
                Amount = amount,
                UserId = userId,
                Description = description,
                Date = DateTime.UtcNow

            });
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            // Implement your password verification logic here
            // This could involve hashing the provided password and comparing it with the stored hash
            return providedPassword == storedHash;
        }


    }
}
