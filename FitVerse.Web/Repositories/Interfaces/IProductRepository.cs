using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        List<Product> GetByCategory(int pageNumber = 1, string categoryName="");
        List<Product> GetByParentCategory(string parentName, int pageNumber = 1, string childCategoryName = "");
        List<Product> SearchByName(int pageNumber = 1, string ProductName = "", string categoryName="");
        public List<Product> FilterByPrice(int pageNumber = 1, decimal price = 0);
    }
}
