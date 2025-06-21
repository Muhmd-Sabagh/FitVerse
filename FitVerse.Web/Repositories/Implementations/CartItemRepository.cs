using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        FitVerseContext _context;
        public CartItemRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }

        public List<CartItem> GetUserCartItems(string userId)
        {
            return _context.CartItems.Where(c => c.UserId == userId).ToList();
        }
        public CartItem GetCartItemByProdId(int PId, string userId)
        {
            // get user Id
            return _context.CartItems.Where(c => c.ProductId == PId && c.UserId == userId).FirstOrDefault();
        }
        public void DeleteByProdId(int PId, string userId)
        {
            var c = _context.CartItems.Where(c => c.ProductId == PId && c.UserId == userId).FirstOrDefault();
            _context.Remove(c);
        }
        public void RemoveAll(string userId)
        {
            //int userId = 8; // get current user Id
            List<CartItem> cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
        }
        public bool AddToCart(int PId, string userId)
        {
            CartItem cartitem = GetCartItemByProdId(PId, userId);
            Product prod = _context.Products.Where(c => c.Id == PId).FirstOrDefault();
            if (cartitem == null)
            {
                cartitem = new CartItem();
                cartitem.ProductId = PId;
                cartitem.UserId = userId;
                cartitem.Quantity = 1;
                _context.CartItems.Add(cartitem);
            }
            else
            {

                if (cartitem.Quantity == prod.StockQuantity)
                    return false;
                cartitem.Quantity++;
                _context.Update(cartitem);

            }
            return true;
        }
        public void DecrementFromCart(int PId, string userId)
        {
            CartItem cartitem = GetCartItemByProdId(PId, userId);
            cartitem.Quantity--;
            _context.Update(cartitem);
            if (cartitem.Quantity == 0)
                DeleteByProdId(PId, userId);
        }
        public decimal getCartCost(string userId)
        {
            List<CartItem> cartItems = GetUserCartItems(userId);
            decimal sum = 0;

            foreach (CartItem item in cartItems)
            {
                sum += item.TotalPrice;
            }
            return sum;
        }
    }
}
