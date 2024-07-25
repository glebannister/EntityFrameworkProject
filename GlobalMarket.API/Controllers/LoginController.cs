using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILoginService _loginService;

        public LoginController(IOptions<JwtSettings> options, ILoginService loginService) 
        {
            _jwtSettings = options.Value;
            _loginService = loginService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserSignInDto userLoginDto)
        {
            var signInResponse = await _loginService.SignInUser(userLoginDto, _jwtSettings);

            return Ok(signInResponse);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUn(UserSignUpDto userLoginDto)
        {
            var signUpSuer = await _loginService.SignUpUser(userLoginDto);

            return Ok(signUpSuer);
        }
    }
}
