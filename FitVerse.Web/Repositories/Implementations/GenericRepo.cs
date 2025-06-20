using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
namespace FitVerse.Web.Repositories.Implementations


{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        
        public GenericRepo(FitVerseContext fit)
        {
            Fit = fit;
        }

        public FitVerseContext Fit { get; set; }

        public T delete(int id)
        {
            throw new NotImplementedException();
        }

        public T find(int id)
        {
            return Fit.Set<T>().Find(id);
        }

        public List<T> getall()
        {
            return Fit.Set<T>().ToList();
        }

        //public T getbyname(string name)
        //{

        //    //return Fit.Set<T>().FirstOrDefault(n=>n.name==name);
        //}

        public T update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
