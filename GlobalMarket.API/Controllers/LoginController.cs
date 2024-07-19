using GlobalMarket.Core.Dto;
using GlobalMarket.Core.Models;
using GlobalMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;

        public LoginController(IConfiguration config, ILoginService loginService) 
        {
            _config = config;
            _loginService = loginService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserSignInDto userLoginDto)
        {
            var jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>();

            var signInResponse = await _loginService.SignInUser(userLoginDto, jwtSettings);

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
