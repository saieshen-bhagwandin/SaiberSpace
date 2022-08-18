using Newtonsoft.Json;
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


        public async Task<string> AddUserAsync(UserRegisterRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/register", request);

            var resultString = await result.Content.ReadAsStringAsync();

            return resultString;

        }

        public async Task<User> LoginAsync(UserLoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user/login", request);

            var resultString = await result.Content.ReadFromJsonAsync<User>();    

            return resultString;

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
