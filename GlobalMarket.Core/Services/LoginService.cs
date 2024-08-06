using System.Security.Cryptography;
using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Exceptions;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Repository;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobalMarket.Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IHashPasswordService _hashPasswordService;
        private readonly AppDbContext _appDbContext;

        public LoginService (ITokenService tokenService, IHashPasswordService hashPasswordService, AppDbContext appDbContext)
        {
            _tokenService = tokenService;
            _hashPasswordService = hashPasswordService;
            _appDbContext = appDbContext;
        }

        public async Task<SignInResponse> SignInUser(UserSignInDto userSignInDto, JwtSettings jwtSettings)
        {
            var signInUser = await GetRegistredUser(userSignInDto);

            if (signInUser is null) 
            {
                throw new UnauthorizedException($"User [{userSignInDto.Name}] did not pass the validation");
            }

            var token = await _tokenService.GetToken(jwtSettings);
            var signInResponse = new SignInResponse
            {
                Name = signInUser.Name,
                Email = signInUser.Email,
                Token = token
            };

            return signInResponse;
        }

        public async Task<User> SignUpUser(UserSignUpDto userSignUpDto)
        {
            var signUpUser = await GetRegistredUser(new UserSignInDto 
            {
                Name = userSignUpDto.Name,
                Password = userSignUpDto.Password,
            });

            if (signUpUser is not null)
            {
                throw new AlreadyExistException($"User {userSignUpDto.Name} with email: [{userSignUpDto.Email}] exists in the DB already");
            }

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashPassword = _hashPasswordService.HashPassword(userSignUpDto.Password, salt);

            var newUser = new User
            {
                Name = userSignUpDto.Name,
                Email = userSignUpDto.Email,
                PasswordHash = hashPassword,
                PasswordSalt = salt
            };

            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return newUser;
        }

        private async Task<User> GetRegistredUser(UserSignInDto userLoginDto) 
        {
            var registredUser = await _appDbContext.Users
                .FirstOrDefaultAsync(user => user.Name == userLoginDto.Name);

            if (registredUser is null) 
            {
                return null;
            }

            var registredUserHashedPassword = _hashPasswordService.HashPassword(userLoginDto.Password, registredUser.PasswordSalt);

            if (!Enumerable.SequenceEqual(registredUserHashedPassword, registredUser.PasswordHash)) 
            {
                return null;
            }

            return registredUser;
        }
    }
}
