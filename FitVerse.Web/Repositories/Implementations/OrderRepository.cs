using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>
    {
        FitVerseContext _context;
        public OrderRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }

    }
}
