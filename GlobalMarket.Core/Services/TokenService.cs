using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GlobalMarket.Core.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> GetToken(JwtSettings jwtSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //new Claim(JwtRegisteredClaimNames.Sub, jwtSettings.Subject),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(ClaimTypes.Name, username)
            //};

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                //claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
