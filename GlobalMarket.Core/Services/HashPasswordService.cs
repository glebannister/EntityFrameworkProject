using GlobalMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GlobalMarket.Core.Services
{
    public class HashPasswordService : IHashPasswordService
    {
        public byte[] HashPassword(string password, byte[] salt)
        {
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return hashed;
        }
    }
}
