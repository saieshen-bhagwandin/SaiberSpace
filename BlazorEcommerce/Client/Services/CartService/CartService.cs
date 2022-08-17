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

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null) {

                cart = new List<CartItem>();

            }

            var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId && x.EditionId == cartItem.EditionId);

            if (sameItem == null)
            {

                cart.Add(cartItem);

            }
            else {

                sameItem.Quantity += cartItem.Quantity;
                
            }

              

            await _localStorage.SetItemAsync("cart",cart);

            var product = await _productService.GetProductByIdAsync(cartItem.ProductId);

            _toastService.ShowSuccess(product.Data.Title, "Added to cart");

            OnChange.Invoke();


        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null) {

                return new List<CartItem>();

            }
            

            return cart;

        }


        public async Task DeleteItem(CartItem item) {

            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            if (cart == null)
            {

                return;

            }

            var cartItem = cart.Find(x => x.ProductId == item.ProductId && x.EditionId == item.EditionId);

            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);

            OnChange.Invoke();


        }

        public async Task EmptyCart()
        {
            await _localStorage.RemoveItemAsync("cart");
            OnChange.Invoke();

        }
    }
}
