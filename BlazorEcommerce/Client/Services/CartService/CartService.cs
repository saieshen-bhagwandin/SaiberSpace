using Blazored.Toast.Services;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IProductService _productService;

        public CartService(ILocalStorageService localStorage,IToastService toastService,IProductService productService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _productService = productService;
        }


        public event Action OnChange;

        public async Task AddToCart(ProductVariant productVariant)
        {
            var cart = await _localStorage.GetItemAsync<List<ProductVariant>>("cart");

            if (cart == null) {

                cart = new List<ProductVariant>();

            }

            cart.Add(productVariant);   

            await _localStorage.SetItemAsync("cart",cart);

            var product = await _productService.GetProductByIdAsync(productVariant.ProductId);

            _toastService.ShowSuccess(product.Data.Title, "Added to cart");

            OnChange.Invoke();


        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var result = new List<CartItem>();

            var cart = await _localStorage.GetItemAsync<List<ProductVariant>>("cart");

            if (cart == null) {

                return result;

            }

            foreach (var item in cart) 
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);

                var cartItem = new CartItem
                {

                    ProductId = product.Data.Id,
                    ProductTitle = product.Data.Title,
                    Image = product.Data.ImageUrl,
                    EditionId = item.EditionId,

                };

                var variant = product.Data.Variants.Find(v => v.EditionId == item.EditionId);

                if (variant != null) {

                    cartItem.EditionName = variant.Edition?.Name;
                    cartItem.Price = variant.Price;
                }


                result.Add(cartItem);

            }

            return result;

        }
    }
}
