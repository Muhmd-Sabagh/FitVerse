using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IProduct: IRepository<Product>
    {
        List<Product> GetByCategory(string categoryName);

    }
}
