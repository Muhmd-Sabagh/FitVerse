using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>
    {
        FitVerseContext _context;
        public ProductRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }
        public List<Product> GetUserProducts(int UId)
        {
            return _context.Products.Where(prod => prod.Id == UId).ToList();
        }

    }
}
