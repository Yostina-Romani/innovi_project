using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        // عرض السلة
        public async Task<IActionResult> Index()
        {
            var customerEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(customerEmail))
                return RedirectToAction("Login", "Customer");

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerEmail);
            if (customer == null)
                return RedirectToAction("Login", "Customer");

            var items = await _cartService.GetCartItems(customer.Id);
            return View(items); // يبحث عن Views/Cart/Index.cshtml
        }

        // إضافة منتج للسلة
        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity)
        {
            var customerEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(customerEmail))
                return RedirectToAction("Login", "Customer");

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerEmail);
            if (customer == null)
                return RedirectToAction("Login", "Customer");

            await _cartService.AddToCart(customer.Id, productId, quantity);
            return RedirectToAction("Index");
        }

        // إزالة منتج من السلة
        public async Task<IActionResult> Remove(int id)
        {
            var customerEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(customerEmail))
                return RedirectToAction("Login", "Customer");

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerEmail);
            if (customer == null)
                return RedirectToAction("Login", "Customer");

            await _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        // الدفع
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var customerEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(customerEmail))
                return RedirectToAction("Login", "Customer");

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerEmail);
            if (customer == null)
                return RedirectToAction("Login", "Customer");

            var cartItems = await _cartService.GetCartItems(customer.Id);
            if (!cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

            // حاليا مجرد Redirect لصفحة تأكيد الدفع
            return RedirectToAction("PaymentConfirmation");
        }

        // صفحة تأكيد الدفع
        public IActionResult PaymentConfirmation()
        {
            var customerEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(customerEmail))
                return RedirectToAction("Login", "Customer");

            var customer = _context.Customers.FirstOrDefault(c => c.Email == customerEmail);
            if (customer == null)
                return RedirectToAction("Login", "Customer");

            ViewBag.CustomerId = customer.Id;
            return View(); // يبحث عن Views/Cart/PaymentConfirmation.cshtml
        }
    }
}
