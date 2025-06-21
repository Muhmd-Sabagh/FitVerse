using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(string categoryName, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Product>> GetByParentCategoryAsync(string parentCategoryName, int pageNumber = 1, int pageSize = 10, string? childCategoryName = null);
        Task<IEnumerable<Product>> SearchByNameAsync(string productName, int pageNumber = 1, int pageSize = 10, string? categoryName = null);
        Task<IEnumerable<Product>> FilterAsync(decimal? maxPrice = null, string? parentCategoryName = null, string? categoryName = null, string? productName = null, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Product>> GetNewArrivalProductsAsync(int count = 5);
    }
}
