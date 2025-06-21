using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FitVerseContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(FitVerseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        //public async Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1)
        //{
            
        //}
        //public void Edit(TEntity entity)
        //{
        //    Db.Set<TEntity>().Update(entity);
        //}

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public List<T> GetAll(int pageNumber = 1)
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(T obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
