using FitVerse.Web.Interfaces;
using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FitVerseContext _context;

        public CategoryRepository(FitVerseContext context)
        {
            _context = context;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.ParentCategory).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            // Include ParentCategory to avoid lazy loading issues if used
            return await _context.Categories.Include(c => c.ParentCategory).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
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
    }
}
