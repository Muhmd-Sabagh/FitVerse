namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity GetById(int id);
        void Add(TEntity obj);
        void Edit(TEntity obj);
        void Delete(int id);
        void Save();
    }
}
