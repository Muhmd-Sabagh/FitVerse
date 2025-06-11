using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories
{
    public class CartItemRepository: GenericRepository<CartItem>
    {
        FitVerseContext _context;
        public CartItemRepository(FitVerseContext _context) : base(_context)
        {

        }
        public List<Product> GetCartByUserId(int UId)
        {
            return Db.Products.Where(prod=> prod.Id == UId).ToList();
        }
         public CartItem GetCartItemByProdId(int PId)
        {
            return _context.CartItems.FirstOrDefault(c => c.ProductId == PId);
        }
        public void DeleteByProdId(int PId)
        {
            var c= _context.CartItems.Where(c => c.ProductId == PId).FirstOrDefault();
            _context.Remove(c);
        }
    }
}
