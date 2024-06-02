using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public static class TokenValidationParametersFactory
{
    public static (TokenValidationParameters tokenValidationParameters, byte[] secureKey) Create(IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"];
        var secureKey = Encoding.UTF8.GetBytes(key);
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(secureKey),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        return (tokenValidationParameters, secureKey);
    }
}