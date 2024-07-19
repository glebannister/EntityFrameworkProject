using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Models;

namespace GlobalMarket.Core.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<SignInResponse> SignInUser(UserLoginDto userSignInDto, JwtSettings jwtSettings);

        public Task<User> SignUpUser(UserLoginDto userSignUpDto);
    }
}
