using Microsoft.AspNetCore.Authorization;
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

            try
            {

                var result = await _orderService.AddOrderAsync(email);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet("getorders/{email}")]
    
        public async Task<ActionResult<ServiceResponse<List<Orders>>>> GetOrders(string email)
        {

            try
            {

                var result = await _orderService.GetOrders(email);

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet("{orderId}")]

        public async Task<ActionResult<Orders>> GetProductById(int orderId)
        {

            try
            {

                var result = await _orderService.GetOrderByIdAsync(orderId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
