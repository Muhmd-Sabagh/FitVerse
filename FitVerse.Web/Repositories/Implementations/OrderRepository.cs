using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(FitVerseContext context) : base(context)
        {
        }

        public async Task<List<string>> GetAllProductNamesFromOrderAsync(int orderId)
        {
            var order = await _dbSet
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return new List<string>();
            }

            return order.OrderItems.Select(item => item.Product?.Name ?? "Unknown Product").ToList();
        }

        public async Task<Order?> GetOrderWithItemsAndProductsAsync(int orderId)
        {
            return await _dbSet
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                    .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync();
        }
    }
}
