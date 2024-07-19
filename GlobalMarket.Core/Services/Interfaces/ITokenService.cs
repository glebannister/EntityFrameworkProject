using GlobalMarket.Core.Models;

namespace GlobalMarket.Core.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GetToken(JwtSettings jwtSettings);
    }
}
