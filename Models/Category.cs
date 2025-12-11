namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;
    }
}
