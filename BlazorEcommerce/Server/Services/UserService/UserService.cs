using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
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

            await _context.SaveChangesAsync();

            return "User successfully created";

        }

        public async Task<string> LoginAsync(UserLoginRequest request)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {

                return "User not found";

            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {

                return "Password is incorrect";

            }

            if (user.VerifiedAt == null)
            {


                return "Not verified";

            }


            return $"Welcome back, {user.Email}! :)"; 
        }

        public async Task<string> VerifyAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if (user == null)
            {

                return "Invalid token";

            }


            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return "User Verified";
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
