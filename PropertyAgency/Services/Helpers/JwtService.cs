using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PropertyAgency.Domain.Enums;

namespace Services.Helpers
{
    public class JwtService
    {
        private readonly byte[] secureKey;
        private readonly TokenValidationParameters tokenValidationParameters;

        public JwtService(byte[] secureKey, TokenValidationParameters tokenValidationParameters)
        {
            this.secureKey = secureKey;
            this.tokenValidationParameters = tokenValidationParameters;
        }

        public string Generate(Guid id, TypeRole role)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(secureKey);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var payload = new JwtPayload(
                issuer: null,
                audience: null,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1)
            );

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


        public JwtSecurityToken Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}