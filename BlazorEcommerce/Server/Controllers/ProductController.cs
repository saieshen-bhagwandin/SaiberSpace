using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]

        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts() {

            var result = await _productService.GetProductsAsync();

            return Ok(result);

        }


        [HttpGet("{productId}")]

        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(int productId)
        {

            var result = await _productService.GetProductByIdAsync(productId);

            return Ok(result);

        }

        [HttpGet("category/{categoryUrl}")]

        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByCategory(string categoryUrl)
        {

            var result = await _productService.GetProductsByCategoryAsync(categoryUrl);

            return Ok(result);

        }


        [HttpGet("search/{searchtext}")]

        public async Task<ActionResult<List<Product>>> SearchProduct(string searchtext)
        {

            var result = await _productService.SearchProduct(searchtext);

            return Ok(result);

        }



    }
}
