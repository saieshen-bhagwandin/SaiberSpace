namespace BlazorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {

        event Action ProductsChanged;
        List<Product> Products { get; set; }

        List<Product> Productsfororders { get; set; }

        Task GetProducts(string? categoryUrl = null);

        Task<ServiceResponse<Product>> GetProductByIdAsync(int productId);

        Task<List<Product>> SearchProduct(string searchtext);

    }
}
