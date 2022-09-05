namespace BlazorEcommerce.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService( DataContext context)
        {
            _context = context;
        }

        public async Task<string> AddOrderAsync(EmailDTO email)
        {
            string thing = "";
            string quantity = "";
            string editionids = "";

            if (_context.Users.Any(u => u.Email == email.user.Email))
            {

                foreach (var item in email.cartItem)
                {
                    thing = thing + "," + item.ProductId;
                    quantity = quantity + "," + item.Quantity;
                    editionids = editionids + "," + item.EditionId;


                };

                string ordernumber =  getordernumber();

                Orders order = new Orders
                {

                    Email = email.user.Email,
                    ProductIds = thing,
                    Date = DateTime.Now,
                    Quantity = quantity,
                    EidtionIds = editionids,
                    OrderNumber = ordernumber
                    
                };

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

                return "order was placed + " + ordernumber;

            }
            else
            {

                return "something went wrong";

            }
        }

        public async Task<Orders> GetOrderByIdAsync(int orderId)
        {
        
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            return order;
        }

        public string getordernumber() {

            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D5");

            return r;

        }

        public async Task<ServiceResponse<List<Orders>>> GetOrders(string email)
        {
            var response = new ServiceResponse<List<Orders>>();
            var orders = await _context.Orders.Where(e => e.Email == email).ToListAsync();

            if (orders == null)
            {

                response.Success = false;
                response.Message = "You don't have any orders";

            }
            else
            {

                response.Data = orders;

            }


            return response;


        }
    }
}
