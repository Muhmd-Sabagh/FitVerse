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
        public decimal getTotalOrdersCost()
        {
            List<Order> orders = GetAll();
            decimal TotalCost = 0;
            foreach (var item in orders)
            {
                TotalCost += item.TotalAmount;
            }
            return TotalCost;


        }

    }
}
