using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        public IConfiguration _configuration;

        public UserService(DataContext context, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<string> AddUserAsync(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {

                return "User already exists";

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

             _emailService.SendEmail(user);

            await _context.SaveChangesAsync();

            return "User successfully created";

        }

        public async Task<User> LoginAsync(UserLoginRequest request)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                var userinvalid = new User();

                userinvalid.Email = "User not found";

                return userinvalid;

            }
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {

                var userinvalid = new User();

                userinvalid.Email = "Password is incorrect";

                return userinvalid;

            }
            else if (user.VerifiedAt == null)
            {


                var userinvalid = new User();

                userinvalid.Email = "User not verified";

                return userinvalid;

            }
            else

            

                return user;
        }

       

        public async Task<string> VerifyAsync(VerifyModel token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token.Token);

            if (user == null)
            {

                return "Invalid token";

            }
            else if (user.VerifiedAt != null)
            {


                return "Account already verified";

            }
            else
            {

                user.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();

                return "User Verified";

            }
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

        private string CreateRandomToken()
        {

            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        }

  
    }
}
