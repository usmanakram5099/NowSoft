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
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public UsersController(IUserService userService, IUserSessionService userSessionService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        [HttpPost]
        [Route("users/signup")]
        public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            var userId = await _userService.CreateUserAsync(userCreateDto);
            if (userId == default)
            {
                return BadRequest();
            }

            // Assuming you have a method to get the user by ID that returns a UserDto
            var userDto = await _userService.GetUserByIdAsync(userId);
            if (userDto == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userDto);
        }


        [HttpPost("users/authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto requestDto)
        {
            var user = await _userService.AuthenticateAsync(requestDto.Username, requestDto.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var token = GenerateJwtToken(user);
            await _userSessionService.RecordSessionAsync(new UserSession
            {
                UserId = user.Id,
                Token = token,
                LoginTime = DateTime.UtcNow,
                IpAddress = "127.0.0.1",
                Device = "Windows 10",
                Browser = "Chrome"
            });

            if (await _userService.IsFirstLoginAsync(user.Id))
            {
                await _userService.AddBalanceAsync(user.Id, 5, "First Login"); // Add 5 GBP to the user's balance
            }

            return Ok(new
            {
                firstname = user.FirstName,
                lastname = user.LastName,
                token = token
            });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = Encoding.ASCII.GetBytes("ijsdfhoasihdfoash#2jhjhsjhb52465465466513215643@$#@");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
