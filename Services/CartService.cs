using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCartItems(int customerId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddToCart(int customerId, int productId, int quantity)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == productId);

            if (item != null)
                item.Quantity += quantity;
            else
                _context.CartItems.Add(new CartItem
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Quantity = quantity
                });

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
