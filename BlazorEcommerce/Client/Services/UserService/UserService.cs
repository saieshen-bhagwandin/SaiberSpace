using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task AddUserAsync(UserRegisterRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/register", request);

           

        }

        public async Task LoginAsync(UserLoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/login", request);



        }

        public async Task VerifyAsync(VerifyModel token)
        {


        

            try
            {
            
                    var result = await _httpClient.PostAsJsonAsync("api/user/verify", token);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);  
                throw;
            }
           


        }
    }
}
