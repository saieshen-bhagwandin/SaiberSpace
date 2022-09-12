using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Authorization;
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


            try
            {
                var result = await _productService.GetProductsAsync();

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet("{productId}")]
      
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(int productId)
        {

            try
            {

                var result = await _productService.GetProductByIdAsync(productId);

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("category/{categoryUrl}")]

        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByCategory(string categoryUrl)
        {

            try
            {
                var result = await _productService.GetProductsByCategoryAsync(categoryUrl);

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet("search/{searchtext}")]

        public async Task<ActionResult<List<Product>>> SearchProduct(string searchtext)
        {

            try
            {
                var result = await _productService.SearchProduct(searchtext);

                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



    }
}
