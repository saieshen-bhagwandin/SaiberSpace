namespace BlazorEcommerce.Server.Services.UserService
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(UserRegisterRequest request);

        public Task<string> LoginAsync(UserLoginRequest request);

        public Task<string> VerifyAsync(string token);



    }
}
