namespace BlazorEcommerce.Client.Services.OrderService
{
    public interface IOrderService
    {

        void AddOrderAsync(EmailDTO email);
    }
}
