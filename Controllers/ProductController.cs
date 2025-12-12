using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public ProductController(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            int customerId = 1; // مؤقت، بعدين تجيبي الـ customerId من تسجيل الدخول
            await _cartService.AddToCart(customerId, productId, quantity);
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            int customerId = 1;
            var items = await _cartService.GetCartItems(customerId);
            return View(items);
        }
    }
}
