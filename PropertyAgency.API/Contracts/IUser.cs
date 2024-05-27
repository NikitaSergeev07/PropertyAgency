using PropertyAgency.API.Dtos.Auth;

namespace PropertyAgency.API.Contracts;

public interface IUser
{
    Task<RegistrationResponse> RegisterUserAsync(RegisterDto regDto);
    Task<LoginResponse> LoginUserAsync(LoginDto logDto);
}