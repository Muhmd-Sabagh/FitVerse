using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using AspNetCoreGeneratedDocument;
using FitVerse.Web.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FitVerse.Web.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>
    {
        int userId = 1;
        FitVerseContext _context;
        public CartItemRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }
        
        public List<CartItem> GetUserCartItems()
        {
            return _context.CartItems.Where(c => c.UserId == userId).ToList();
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
        public void RemoveAll()
        {
            //int userId = 8; // get current user Id
            List <CartItem> cartItems= _context.CartItems.Where(c => c.UserId == userId).ToList();
            _context.CartItems.RemoveRange(cartItems);
        }
        public bool AddToCart(int PId)
        {
            CartItem cartitem = GetCartItemByProdId(PId);
            Product prod = _context.Products.Where(c => c.Id == PId).FirstOrDefault();
            if (cartitem == null)
            {
                cartitem.ProductId = PId;
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
        public void DecrementFromCart(int PId)
        {
            CartItem cartitem = GetCartItemByProdId(PId);
            cartitem.Quantity--;
            _context.Update(cartitem);
            if(cartitem.Quantity == 0)
                DeleteByProdId(PId);
        }
        public decimal getCartCost()
        {
            List<CartItem> cartItems = GetUserCartItems();
            decimal sum = 0;

            foreach (CartItem item in cartItems)
            {
                sum += item.TotalPrice;
            }
            return sum;
        }
    }
}
