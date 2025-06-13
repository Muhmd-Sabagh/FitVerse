using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>
    {
        FitVerseContext _context;
        public ProductRepository(FitVerseContext _context) : base(_context)
        {
        }

    }
}
