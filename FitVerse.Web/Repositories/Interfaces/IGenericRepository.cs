using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public interface IGenericRepository<TEntity> 
    {
        public List<TEntity> GetAll();

        public TEntity GetById(int Id);

        public void Add(TEntity entity);

        public void Update(TEntity entity);

        public void DeleteById(int Id);

    }
}
