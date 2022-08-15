namespace BlazorEcommerce.Client.Services.UserService
{
    public interface IUserService
    {

        public Task AddUserAsync(UserRegisterRequest request);

        public Task LoginAsync(UserLoginRequest request);

        public Task VerifyAsync(VerifyModel token);

    }
}
