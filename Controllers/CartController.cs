using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int customerId)
        {
            var items = await _cartService.GetCartItems(customerId);
            return View(items);
        }

        public async Task<IActionResult> Add(int customerId, int productId, int quantity)
        {
            await _cartService.AddToCart(customerId, productId, quantity);
            return RedirectToAction("Index", new { customerId });
        }

        public async Task<IActionResult> Remove(int cartItemId)
        {
            await _cartService.RemoveFromCart(cartItemId);
            return RedirectToAction("Index");
        }
    }
}
