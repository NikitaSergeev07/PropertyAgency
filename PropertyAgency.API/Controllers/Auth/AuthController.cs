using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos.Auth;
using PropertyAgency.Domain.Entities;
using Services.Helpers;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace PropertyAgency.API.Controllers
{
    [ApiController]
    [Route("")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly JwtService _jwtService;

        public AuthController(IUsersService usersService, JwtService jwtService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto regDto)
        {
            var newUser = new User
            {
                UserName = regDto.UserName,
                Email = regDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(regDto.Password)
            };
            var createdUser = await _usersService.CreateUser(newUser);
            return Created("success", createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logDto)
        {
            var user = await _usersService.GetByEmail(logDto.Email);
            if (user == null) return BadRequest(new { message = "Invalid credentials" });

            if (!BCrypt.Net.BCrypt.Verify(logDto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> User()
        {
            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            JwtSecurityToken jwtToken;
            try
            {
                jwtToken = _jwtService.Verify(token);
            }
            catch
            {
                return Unauthorized();
            }

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            Guid userId = Guid.Parse(jwtToken.Issuer);
            var user = await _usersService.GetById(userId);
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}
