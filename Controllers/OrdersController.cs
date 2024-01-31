using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService service, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = service;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;

        }
        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _ordersService.GetordersByUserIdAsync(userId);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RmoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("CompleteOrder");
        }
    }
}
