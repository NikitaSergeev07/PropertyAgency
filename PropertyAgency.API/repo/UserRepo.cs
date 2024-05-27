using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PropertyAgency.API.Contracts;
using PropertyAgency.API.Dtos.Auth;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.repo;

public class UserRepo : IUser
{
    private readonly IUsersService _usersService;
    private readonly IConfiguration _configuration;

    public UserRepo(IUsersService usersService, IConfiguration configuration)
    {
        _usersService = usersService;
        _configuration = configuration;
    }
    public async Task<RegistrationResponse> RegisterUserAsync(RegisterDto regDto)
    {
        var getUser = await _usersService.GetByEmail(regDto.Email);
        if (getUser != null)
        {
            return new RegistrationResponse(false, "Пользователь уже существует");
        }

        var newUser = new User
        {
            UserName = regDto.UserName,
            Email = regDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(regDto.Password)
        };
        await _usersService.CreateUser(newUser);
        return new RegistrationResponse(true, "Регистрация прошла успешно");
    }

    public async Task<LoginResponse> LoginUserAsync(LoginDto logDto)
    {
        var getUser = await _usersService.GetByEmail(logDto.Email);
        if (getUser == null) return new LoginResponse(false, "Пользователь не найден");
        bool checkPassword = BCrypt.Net.BCrypt.Verify(logDto.Password, getUser.Password);
        if (checkPassword) 
            return new LoginResponse(true, "Вы залогинены успешно", GenerateJwtToken(getUser));
        else 
            return new LoginResponse(false, "Неверные учетные данные");


    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}