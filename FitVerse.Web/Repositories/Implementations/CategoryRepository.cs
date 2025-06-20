using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FitVerse.Web.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        FitVerseContext db;
        public CategoryRepository(FitVerseContext _db)
        {
            db = _db;
        }
        public List<Category> GetParentCategories()
        {
            return db.Categories.Where(c => c.ParentCategory == null)
                .Include(c => c.SubCategories)
                .ToList();
        }

        public List<Category> GetChildCategories(int parentCategoryId)
        {
            return db.Categories.Where(c => c.ParentCategoryId == parentCategoryId)
                .Include(c => c.ParentCategory)
                .ToList();
        }
        public void Add(Category obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Category obj)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll(int pageNumber = 1)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
