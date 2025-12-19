using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Customer/Register
        public IActionResult Register() => View();

        // POST: /Customer/Register
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(customer);
        }

        // GET: /Customer/Login
        public IActionResult Login() => View();

        // POST: /Customer/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var customer = _context.Customers
                .FirstOrDefault(c => c.Email == email && c.Password == password);

            if (customer != null)
            {
                // حفظ بيانات المستخدم في Session
                HttpContext.Session.SetInt32("CustomerId", customer.Id);
                HttpContext.Session.SetString("UserEmail", customer.Email);

                // التوجيه مباشرة لصفحة المنتجات
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: /Customer/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
