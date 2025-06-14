using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using AspNetCoreGeneratedDocument;
using FitVerse.Web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FitVerse.Web.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        int userId = 8;
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
           // get user Id
            return _context.CartItems.Where(c => c.ProductId == PId && c.UserId==userId).FirstOrDefault();
        }
        public void DeleteByProdId(int PId)
        {
            var c = _context.CartItems.Where(c => c.ProductId == PId).FirstOrDefault();
            _context.Remove(c);
        }
        public List<CartItem> GetUserCart()
        {
            //int userId = 8;
            return _context.CartItems.Where(c => c.UserId == userId).ToList();
        }
        public void RemoveAll()
        {
            //int userId = 8; // get current user Id
            List <CartItem> cartItems= _context.CartItems.Where(c => c.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
        }
        public bool AddToCart(int PId)
        {
            // need to check // with home controller when add to cart
            //CartItem cartitem = _context.CartItems.Where(c => c.ProductId == PId).FirstOrDefault();
            CartItem cartitem = GetCartItemByProdId(PId);
            Product prod = _context.Products.Where(c => c.Id == PId).FirstOrDefault();
            if (cartitem == null)
            {
                cartitem.ProductId = PId;
                _context.CartItems.Add(cartitem);
            }
            else
            {
                // ✅
                if (cartitem.Quantity == prod.StockQuantity)
                    return false;
                cartitem.Quantity++;
                _context.Update(cartitem);
                    
            }
            return true;
        }
        public void DecrementFromCart(int PId)
        {
            CartItem cartitem = GetCartItemByProdId(PId);
            cartitem.Quantity--;
            _context.Update(cartitem);
            if(cartitem.Quantity == 0)
                DeleteByProdId(PId);
        }
    }
}
