namespace BlazorEcommerce.Server.Services.OrderService
{
    public interface IOrderService
    {

        public Task<string> AddOrderAsync(EmailDTO email);

        Task<ServiceResponse<List<Orders>>> GetOrders(string email);

        Task<Orders> GetOrderByIdAsync(int orderId);

    }
}
