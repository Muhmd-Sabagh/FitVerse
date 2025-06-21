using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(FitVerseContext context) : base(context)
        {
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _dbSet
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Product) // Eager load product details for order items
                .ToListAsync();
        }
    }
}
