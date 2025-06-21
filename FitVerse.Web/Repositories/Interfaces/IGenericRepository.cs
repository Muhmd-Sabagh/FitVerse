
namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<(IEnumerable<TEntity> entities, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);
        Task DeleteAsync(int id);
    }
}
