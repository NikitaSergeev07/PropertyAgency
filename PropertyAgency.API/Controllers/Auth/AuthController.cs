using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Contracts;
using PropertyAgency.API.Dtos.Auth;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers.Auth;

public class AuthController : ControllerBase
{
    private readonly IUser _user;

    public AuthController(IUser user)
    {
        _user = user;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LogUserIn(LoginDto loginDto)
    {
        var result = await _user.LoginUserAsync(loginDto);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterDto>> RegisterUser(RegisterDto registerDto)
    {
        var result = await _user.RegisterUserAsync(registerDto);
        return Ok(result);
    }
}