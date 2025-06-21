using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<List<CartItem>> GetUserCartItemsAsync(string userId);
        Task<CartItem?> GetCartItemByProdIdAsync(string userId, int productId);
        Task DeleteByProdIdAsync(string userId, int productId);
        Task RemoveAllAsync(string userId);
        Task<bool> AddToCartAsync(string userId, int productId, int quantity = 1);
        Task DecrementFromCartAsync(string userId, int productId);
        Task<decimal> GetCartCostAsync(string userId);
    }
}
