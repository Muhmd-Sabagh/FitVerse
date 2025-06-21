using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        List<Product> GetByCategory(int pageNumber = 1, string categoryName="");
        List<Product> GetByParentCategory(string parentName, int pageNumber = 1, string childCategoryName = "");
        List<Product> SearchByName(int pageNumber = 1, string ProductName = "", string categoryName="");
        List<Product> Filter(int pageNumber = 1, decimal price = 0, string parentName = "", string categoryName = "", string ProductName = "");
        List<Product> GetByParentCategoryId(int parentId);
        List<Product> GetNewArrivalProducts(int pageNumber = 1);
    }
}
