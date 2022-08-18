namespace BlazorEcommerce.Server.Services.OrderService
{
    public interface IOrderService
    {

        public void AddOrderAsync(EmailDTO email);
    }
}
