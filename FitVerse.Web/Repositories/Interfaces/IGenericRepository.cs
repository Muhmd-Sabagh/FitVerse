namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        List<TEntity> GetAll(int pageNumber = 1);
        TEntity GetById(int id);
        void Add(TEntity obj);
        void Edit(TEntity obj);
        void Delete(int id);

    }
}
