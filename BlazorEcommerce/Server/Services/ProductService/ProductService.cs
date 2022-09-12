namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.Include(p => p.Variants).ThenInclude(v => v.Edition).FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {

                response.Success = false;
                response.Message = "Sorry, but this product does not exist";

            }
            else {

                response.Data = product;
            
            }


            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {

            var products = await _context.Products.FromSqlRaw("GetAllProducts").ToListAsync();

            foreach (var item in products)
            {
               var listofvariants = await _context.ProductVariant.FromSqlRaw($"GetAllVariants {item.Id}").ToListAsync();


                item.Variants.Concat(listofvariants);

            }
           

            var response = new ServiceResponse<List<Product>>()
            {

                Data = products,

            };


           return response; 
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {

                Data = await _context.Products.Include(p => p.Variants).Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync()


            };

            return response;
        }

        public async Task<List<Product>> SearchProduct(string searchtext)
        {
            return await _context.Products.FromSqlRaw($"Search {searchtext}").ToListAsync();
        }
    }
}
