using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<string>> GetAllProductNamesFromOrderAsync(int orderId);
        Task<Order?> GetOrderWithItemsAndProductsAsync(int orderId);
    }
}
