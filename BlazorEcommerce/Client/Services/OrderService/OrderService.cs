namespace BlazorEcommerce.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;

        public OrderService( HttpClient http)
        {

            _http = http;
        }

        public async void AddOrderAsync(EmailDTO email)
        {
            var result = await _http.PostAsJsonAsync("api/order/add", email);
        }
    }
}
