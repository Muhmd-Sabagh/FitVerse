using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(FitVerseContext context) : base(context)
        {
        }

        public async Task<List<CartItem>> GetUserCartItemsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new List<CartItem>();
            }
            return await _dbSet.Where(c => c.UserId == userId).Include(c => c.Product).ToListAsync();
        }

        public async Task<CartItem?> GetCartItemByProdIdAsync(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return await _dbSet.Where(c => c.ProductId == productId && c.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task DeleteByProdIdAsync(string userId, int productId)
        {
            var cartItem = await GetCartItemByProdIdAsync(userId, productId);
            if (cartItem != null)
            {
                _dbSet.Remove(cartItem);
            }
        }

        public async Task RemoveAllAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            List<CartItem> cartItems = await _dbSet.Where(c => c.UserId == userId).ToListAsync();
            _dbSet.RemoveRange(cartItems);
        }

        public async Task<bool> AddToCartAsync(string userId, int productId, int quantity = 1)
        {
            if (string.IsNullOrEmpty(userId))
            {
                // User not logged in, cannot add to cart
                return false;
            }

            var cartItem = await GetCartItemByProdIdAsync(userId, productId);
            Product? product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

            if (product == null)
            {
                return false; // Product does not exist
            }

            if (cartItem == null)
            {
                if (quantity <= product.StockQuantity)
                {
                    cartItem = new CartItem
                    {
                        ProductId = productId,
                        UserId = userId,
                        Quantity = quantity,
                        CreatedAt = DateTime.Now
                    };
                    await _dbSet.AddAsync(cartItem);
                    return true;
                }
                else
                {
                    // Not enough stock for initial add
                    return false;
                }
            }
            else
            {
                if (cartItem.Quantity + quantity <= product.StockQuantity)
                {
                    cartItem.Quantity += quantity;
                    _dbSet.Update(cartItem);
                    return true;
                }
                else
                {
                    // Not enough stock for increment
                    return false;
                }
            }
        }

        public async Task DecrementFromCartAsync(string userId, int productId)
        {
            var cartItem = await GetCartItemByProdIdAsync(userId, productId);
            if (cartItem != null)
            {
                cartItem.Quantity--;
                _dbSet.Update(cartItem);

                if (cartItem.Quantity <= 0)
                {
                    // If quantity drops to 0 or below, remove the item
                    _dbSet.Remove(cartItem);
                }
            }
        }

        public async Task<decimal> GetCartCostAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return 0;
            }
            var cartItems = await _dbSet
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            decimal sum = 0;
            foreach (var item in cartItems)
            {
                if (item.Product != null)
                {
                    sum += item.Product.EffectivePrice * item.Quantity;
                }
            }
            return sum;
        }
    }
}
