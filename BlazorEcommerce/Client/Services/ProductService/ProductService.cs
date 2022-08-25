namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Product> Productsfororders { get; set; } = new List<Product>();

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");

            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var allresult = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");

            if (allresult != null && allresult.Data != null)
            {
                Productsfororders = allresult.Data;
            }

            var result = categoryUrl == null? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product"): await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null)
            {
                Products = result.Data;
                
            }

            ProductsChanged.Invoke();
        }

        public async Task<List<Product>> SearchProduct(string searchtext)
        {
           return await _http.GetFromJsonAsync<List<Product>>($"api/Product/Search/{searchtext}"); 
        }
    }
}
