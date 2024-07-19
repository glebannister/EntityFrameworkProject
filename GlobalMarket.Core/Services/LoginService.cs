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

        public async Task<SignInResponse> SignInUser(UserLoginDto userSignInDto, JwtSettings jwtSettings)
        {
            var signInUser = await ValidateUser(userSignInDto);

            if (signInUser is null) 
            {
                throw new UnauthorizedException($"User [{userSignInDto.Name}] with email: [{userSignInDto.Email}] did not pass the validation");
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

        public async Task<User> SignUpUser(UserLoginDto userSignUpDto)
        {
            var signUpUser = await ValidateUser(userSignUpDto);

            if (signUpUser is not null)
            {
                throw new ConflictException($"User {userSignUpDto.Name} with email: [{userSignUpDto.Email}] exists in the DB already");
            }

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashPassword = _hashPasswordService.HashPassword(userSignUpDto.Password, salt);

            var newUser = new User
            {
                Name = userSignUpDto.Name,
                Email = userSignUpDto.Email,
                Password = hashPassword,
                Salt = salt
            };

            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return newUser;
        }

        private async Task<User> ValidateUser(UserLoginDto userLoginDto) 
        {
            var validateUser = await _appDbContext.Users
                .FirstOrDefaultAsync(user => user.Name == userLoginDto.Name);

            if (validateUser is null) 
            {
                return null;
            }

            var validateUserHashedPassword = _hashPasswordService.HashPassword(userLoginDto.Password, validateUser.Salt);

            return await _appDbContext.Users
                .FirstOrDefaultAsync(user => user.Email == userLoginDto.Email && user.Password == validateUserHashedPassword);
        }
    }
}
