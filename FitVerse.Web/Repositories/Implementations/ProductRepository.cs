using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        FitVerseContext db;
        int pageSize = 20;
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
            var product = GetById(id);
            if (product != null)
                db.Products.Remove(product);
        }

        public void Edit(Product obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public List<Product> GetAll(int pageNumber = 1)
        {
            return db.Products
                      .Include(p => p.Category)
                      .ThenInclude(c => c.ParentCategory)
                      .OrderBy(p => p.Id)
                      .Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();
        }

        public List<Product> GetByCategory(int pageNumber = 1, string categoryName = "")
        {
            var query = db.Products
                .Include(p => p.Category)
                  .ThenInclude(c => c.ParentCategory)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var lowerCat = categoryName.ToLower();
                query = query.Where(p => p.Category.Name.ToLower() == lowerCat);
            }

            return query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Product GetById(int id)
        {
            return db.Products
                 .Include(p => p.Category)
                   .ThenInclude(c => c.ParentCategory)
                 .SingleOrDefault(p => p.Id == id);
        }

        public List<Product> GetByParentCategory(
            string parentName,
            int pageNumber = 1,
            string childCategoryName = "")
        {
            var query = db.Products
                .Include(p => p.Category)
                  .ThenInclude(c => c.ParentCategory)
                .Where(p => p.Category.ParentCategory.Name == parentName);

            if (!string.IsNullOrWhiteSpace(childCategoryName))
            {
                var lowerChild = childCategoryName.ToLower();
                query = query.Where(p => p.Category.Name.ToLower() == lowerChild);
            }

            return query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Product> SearchByName(int pageNumber = 1, string ProductName = "", string categoryName = "")
        {
            var query = db.Products
                .Include(p => p.Category)
                  .ThenInclude(c => c.ParentCategory)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(ProductName))
            {
                var lowerName = ProductName.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerName));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var lowerCat = categoryName.ToLower();
                query = query.Where(p => p.Category.Name.ToLower() == lowerCat);
            }

            return query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Product> FilterByPrice(int pageNumber = 1, decimal maxPrice = 0)
        {
            var query = db.Products
                .Include(p => p.Category)
                  .ThenInclude(c => c.ParentCategory)
                .AsQueryable();

            return query
                .Where(p=> p.Price <= maxPrice)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
