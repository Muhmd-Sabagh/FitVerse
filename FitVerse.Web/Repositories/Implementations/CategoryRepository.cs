using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FitVerseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.SubCategories)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryWithSubcategoriesAsync(int categoryId)
        {
            return await _dbSet
                .Where(c => c.Id == categoryId)
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetParentCategoryNameByChildProductIdAsync(int productId)
        {
            // Find the product and then its category and parent category
            var product = await _context.Products
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product?.Category?.ParentCategory != null)
            {
                return product.Category.ParentCategory.Name;
            }
            return null;
        }

        public new async Task<IEnumerable<Category>> GetAllAsync(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return await _dbSet
                .Include(c => c.ParentCategory)
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
