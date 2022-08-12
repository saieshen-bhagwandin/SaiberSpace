using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request) {

            if (_context.Users.Any(u => u.Email == request.Email)) {

                return BadRequest("User already exists");
            
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);


            var user = new User
            {

                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()

            };


            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("User successfully created");

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {

           var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null) {

                return BadRequest("User not found");
            
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {

                return BadRequest("Password is incorrect");

            }

            if (user.VerifiedAt == null) {


                return BadRequest("Not verified");

            }


            return Ok($"Welcome back, {user.Email}! :)");
        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {

                return BadRequest("Invalid token");

            }

            
            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();


            return Ok("User Verified");
        }




        private void CreatePasswordHash(string passsword, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
            }

        }

        private bool VerifyPasswordHash(string passsword, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));

                return computedHash.SequenceEqual(passwordHash);
            }

        }

        private string CreateRandomToken() {

            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        
        }


    }
}
