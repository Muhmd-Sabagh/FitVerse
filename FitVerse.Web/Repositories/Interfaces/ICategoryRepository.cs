using FitVerse.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<Category?> GetCategoryWithSubcategoriesAsync(int categoryId);
        Task<string?> GetParentCategoryNameByChildProductIdAsync(int productId);
    }
}
