using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        public List<Category> GetParentCategories();
        public List<Category> GetChildCategories(int parentCategoryId);
    }
}
