namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? TrackingNumber { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
