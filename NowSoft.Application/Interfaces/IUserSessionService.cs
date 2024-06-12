using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NowSoft.Application.Dtos;
using NowSoft.Domain.Models;

namespace NowSoft.Application.Interfaces
{
    public interface IUserSessionService
    {
        Task<UserSessionDto> GetUserSessionByTokenAsync(string token);
        Task<int> CreateUserSessionAsync(UserSessionCreateDto userSessionCreateDto);
        Task RecordSessionAsync(UserSession userSession);
    }
}
