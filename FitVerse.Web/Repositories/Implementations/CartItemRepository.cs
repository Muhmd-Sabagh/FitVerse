using FitVerse.Web.Models;

using Microsoft.EntityFrameworkCore;


namespace FitVerse.Web.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        FitVerseContext _context;
        public CartItemRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }
        public List<Product> GetUserCartItems(int UId)
        {
            return Db.Products.Where(prod => prod.Id == UId).ToList();
        }
        public CartItem GetCartItemByProdId(int PId)
        {
            int userId = 1; // get user Id
            return _context.CartItems.Where(c => c.ProductId == PId && c.UserId==userId).FirstOrDefault();
        }
        public void DeleteByProdId(int PId)
        {
            var c = _context.CartItems.Where(c => c.ProductId == PId).FirstOrDefault();
            _context.Remove(c);
        }
        public List<CartItem> GetUserCart()
        {
            int UserId = 1;
            return _context.CartItems.Where(c => c.UserId == UserId).ToList();
        }
        public void RemoveAll()
        {
            int userId = 1; // get current user Id
            List <CartItem> cartItems= _context.CartItems.Where(c => c.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
        }
    }
}
