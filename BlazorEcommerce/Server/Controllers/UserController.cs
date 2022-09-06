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

            try
            {
                var result = await _userService.AddUserAsync(request);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        [HttpPost("login")]
        public async Task<User> Login(UserLoginRequest request)
        {

            try
            {
                var result = await _userService.LoginAsync(request);

                if (result.Id == 0)
                {

                    return result;

                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpPost("verify")]
        public async Task<string> Verify(VerifyModel token)
        {

            try
            {
                var result = await _userService.VerifyAsync(token);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



    }
}
