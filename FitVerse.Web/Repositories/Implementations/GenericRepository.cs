using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
    {
        public FitVerseContext Db { get; }
        public GenericRepository(FitVerseContext _db)
        {
            Db = _db;
        }
        public List<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }
        public TEntity GetById(int Id)
        {
            return Db.Set<TEntity>().Find(Id);
        }
        public void Add(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            Db.Set<TEntity>().Update(entity);
        }

        public void DeleteById(int Id)
        {
            Db.Set<TEntity>().Remove(GetById(Id));
        }

       
    }
}
