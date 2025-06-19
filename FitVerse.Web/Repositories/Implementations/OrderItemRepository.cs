    using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using AspNetCoreGeneratedDocument;
using FitVerse.Web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        int userId = 1;
        FitVerseContext _context;
        public OrderItemRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }
        public List<CartItem> GetUserCartItems(int UId)
        {
            return _context.CartItems.Where(c=> c.UserId == UId).ToList();
        }
        
    }
}
