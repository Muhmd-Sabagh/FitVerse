using E_commerce_Khotwa.Models;

namespace E_commerce_Khotwa.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        public MarkITIContext Db { get; }
        public GenericRepository(MarkITIContext _db)
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
        //public void DeleteAll() {
        //    Db.Set<TEntity>().RemoveAll();
        //}



    }
}
