using FitVerse.Web.Models;

namespace FitVerse.Web.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId);
        Task<CartItem> GetCartItemByProductIdAndUserIdAsync(int productId, string userId); // For updating quantity
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int id);
        Task DeleteCartItemsByUserIdAsync(string userId);
    }
}
