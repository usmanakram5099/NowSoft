using NowSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Domain.Interfaces
{
    public interface IUserSessionRepository
    {
        Task<int> CreateSessionAsync(UserSession session);
        Task<UserSession> GetSessionByTokenAsync(string token);
        Task<int> GetLoginCountByUserIdAsync(int userId);
    }
}
