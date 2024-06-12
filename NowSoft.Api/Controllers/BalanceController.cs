using Microsoft.AspNetCore.Mvc;
using NowSoft.Application.Dtos;
using NowSoft.Application.Interfaces;
using NowSoft.Application.Services;
using NowSoft.Domain.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace NowSoft.Api.Controllers
{
    [ApiController]
    [Route("balance")]
    public class BalanceController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IBalanceHistoryService _balanceHistoryService;

        public BalanceController(IUserService userService, IUserSessionService userSessionService, IBalanceHistoryService balanceHistoryService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
            _balanceHistoryService = balanceHistoryService;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetBalanceByToken(string token)
        {
            var userSessionDto= await _userSessionService.GetUserSessionByTokenAsync(token);
            if (userSessionDto == null)
            {
                return NotFound();
            }
            var balance = await _userService.GetUserBalanceByIdAsync(userSessionDto.UserId);
            
            return Ok(balance);
        }

    }
}
