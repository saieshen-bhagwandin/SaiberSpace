namespace BlazorEcommerce.Server.Services.ProductService
{
    public interface IProductService
    {

        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<Product>> GetProductByIdAsync(int productId);

        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(String categoryUrl);

        Task<List<Product>> SearchProduct(string searchtext);
    }
}
