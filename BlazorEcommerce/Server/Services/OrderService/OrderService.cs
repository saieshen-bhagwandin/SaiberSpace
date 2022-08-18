namespace BlazorEcommerce.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService( DataContext context)
        {
            _context = context;
        }

        public async void AddOrderAsync(EmailDTO email)
        {
            string thing = "";
            string quantity = "";

            if (_context.Users.Any(u => u.Email == email.user.Email))
            {

                foreach (var item in email.cartItem)
                {
                    thing = thing + "," + item.ProductId;
                    quantity = quantity + "," + item.Quantity;
                };

                Orders order = new Orders
                {
                   
                    Email = email.user.Email,
                    ProductIds = thing,
                    Date = DateTime.Now,
                    Quantity = quantity
                };

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

            }
        }

    }
}
