using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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

        public string Generate(Guid id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(secureKey);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.UtcNow.AddDays(1));
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