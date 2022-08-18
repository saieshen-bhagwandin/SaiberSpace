using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<string> Register(UserRegisterRequest request) {

            var result = await _userService.AddUserAsync(request);

            return result;


        }


        [HttpPost("login")]
        public async Task<User> Login(UserLoginRequest request)
        {

            var result = await _userService.LoginAsync(request);

            if (result.Id == 0) {

                return result;

            }

            return result;

        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify(VerifyModel token)
        {

            var result = await _userService.VerifyAsync(token);

            return Ok(result);
        }



    }
}
