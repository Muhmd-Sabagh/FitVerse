using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepository : IProduct
    {
        FitVerseContext db;
        public ProductRepository(FitVerseContext _db)
        {
            db = _db;
        }
        public void Add(Product obj)
        {
            db.Products.Add(obj);
        }

        public void Delete(int id)
        {
            db.Products.Remove(GetById(id));
        }

        public void Edit(Product obj)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public List<Product> GetByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
