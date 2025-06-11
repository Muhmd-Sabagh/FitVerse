using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(FitVerseContext _db) : base(_db)
        {
        }

    }
}
