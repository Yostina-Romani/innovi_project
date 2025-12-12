using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
       
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Material { get; set; } = null!;


        public ICollection<OrderItem> OrderItems { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
