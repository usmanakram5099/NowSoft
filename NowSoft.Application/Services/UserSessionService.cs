using NowSoft.Application.Dtos;
using NowSoft.Application.Interfaces;
using NowSoft.Domain.Interfaces;
using NowSoft.Domain.Models;
using System.Threading.Tasks;

namespace NowSoft.Application.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionService(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public async Task<UserSessionDto> GetUserSessionByTokenAsync(string token)
        {
            var userSession = await _userSessionRepository.GetSessionByTokenAsync(token);
            if (userSession == null) return null;

            return new UserSessionDto
            {
                Id = userSession.Id,
                UserId = userSession.UserId,
                Token = userSession.Token,
                LoginTime = userSession.LoginTime,
                IpAddress = userSession.IpAddress,
                Device = userSession.Device,
            };
        }

        public async Task<int> CreateUserSessionAsync(UserSessionCreateDto userSessionCreateDto)
        {
            var userSession = new UserSession
            {
                UserId = userSessionCreateDto.UserId,
                Token = userSessionCreateDto.Token,
                LoginTime = userSessionCreateDto.LoginTime,
                IpAddress = userSessionCreateDto.IpAddress,
                Device = userSessionCreateDto.Device,
            };

            return await _userSessionRepository.CreateSessionAsync(userSession);
        }

        public async Task RecordSessionAsync(UserSession userSession)
        {
            // Logic to record the user session
            await _userSessionRepository.CreateSessionAsync(userSession);
        }
    }
}
