using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;

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
        public void Edit(TEntity entity)
        {
            Db.Set<TEntity>().Update(entity);
        }

        public void Delete(int Id)
        {
            Db.Set<TEntity>().Remove(GetById(Id));
        }

        public List<TEntity> GetAll(int pageNumber = 1)
        {
            return Db.Set<TEntity>().Take(20).ToList();
        }

        
        


    }
}
