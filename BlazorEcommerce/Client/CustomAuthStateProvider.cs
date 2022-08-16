using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorEcommerce.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var state = new AuthenticationState(new ClaimsPrincipal());


            string email = await _localStorage.GetItemAsStringAsync("userinfo");

            if (!string.IsNullOrEmpty(email)) {

                var identity = new ClaimsIdentity(new[] {

                 new Claim(ClaimTypes.Email,email)


                },"test authentication type");


                state = new AuthenticationState(new ClaimsPrincipal(identity));
            
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
