namespace PropertyAgency.API.Dtos.Auth;

public record LoginResponse(bool flag, string Message=null!, string Token=null!);