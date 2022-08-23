using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost("add")]
        public async Task<string> AddToOrder(EmailDTO email)
        {
           var result = await _orderService.AddOrderAsync(email);

            return result;

        }


        [HttpGet("getorders/{email}")]

        public async Task<ActionResult<ServiceResponse<List<Orders>>>> GetOrders(string email)
        {

            var result = await _orderService.GetOrders(email);

            return Ok(result);

        }




    }
}
