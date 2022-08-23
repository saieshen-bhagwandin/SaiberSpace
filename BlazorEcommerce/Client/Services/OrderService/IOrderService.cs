namespace BlazorEcommerce.Client.Services.OrderService
{
    public interface IOrderService
    {

        event Action OrdersChanged;
        List<Orders> Orders { get; set; }

        Task<string> AddOrderAsync(EmailDTO email);

        Task GetOrder(string email);
    }
}
