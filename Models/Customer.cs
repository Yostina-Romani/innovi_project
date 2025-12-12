
namespace WebApplication1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;  // أضفنا الباسورد
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}