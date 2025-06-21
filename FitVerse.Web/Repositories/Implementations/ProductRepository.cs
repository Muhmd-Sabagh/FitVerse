using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(FitVerseContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Product>> GetAllAsync(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return await _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public new async Task<(IEnumerable<Product> entities, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _dbSet.CountAsync();
            var entities = await _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (entities, totalCount);
        }

        public new async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName, int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var query = _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .Where(p => p.Category != null && p.Category.Name.ToLower() == categoryName.ToLower());

            return await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByParentCategoryAsync(string parentCategoryName, int pageNumber = 1, int pageSize = DefaultPageSize, string? childCategoryName = null)
        {
            var query = _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .Where(p => p.Category != null && p.Category.ParentCategory != null && p.Category.ParentCategory.Name.ToLower() == parentCategoryName.ToLower());

            if (!string.IsNullOrEmpty(childCategoryName))
            {
                query = query.Where(p => p.Category.Name.ToLower() == childCategoryName.ToLower());
            }

            return await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string productName, int pageNumber = 1, int pageSize = DefaultPageSize, string? categoryName = null)
        {
            var query = _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .Where(p => p.Name.ToLower().Contains(productName.ToLower()));

            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p => p.Category != null && p.Category.Name.ToLower() == categoryName.ToLower());
            }

            return await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterAsync(decimal? maxPrice = null, string? parentCategoryName = null, string? categoryName = null, string? productName = null, int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var query = _dbSet
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .AsQueryable();

            if (maxPrice.HasValue && maxPrice.Value > 0)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(parentCategoryName))
            {
                query = query.Where(p => p.Category != null && p.Category.ParentCategory != null && p.Category.ParentCategory.Name.ToLower() == parentCategoryName.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                query = query.Where(p => p.Category != null && p.Category.Name.ToLower() == categoryName.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(p => p.Name.ToLower().Contains(productName.ToLower()));
            }

            return await query
                .OrderBy(p => p.Price)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewArrivalProductsAsync(int count = 5)
        {
            return await _dbSet
                .Where(p => p.IsNewArrival)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}
