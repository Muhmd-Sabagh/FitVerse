using FitVerse.Web.Models;


namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        FitVerseContext _context;
        public OrderItemRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }
        public List<CartItem> GetUserCartItems(string UId)
        {
            return _context.CartItems.Where(c=> c.UserId == UId).ToList();
        }

    }
}
