using FitVerse.Web.Repositories;
namespace FitVerse.Web.GenericRepos

{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        public GenericRepo() { }

        public T delete(int id)
        {
            throw new NotImplementedException();
        }

        public T find(int id)
        {
            throw new NotImplementedException();
        }

        public T getall()
        {
            throw new NotImplementedException();
        }

        public T getbyname(string name)
        {
            throw new NotImplementedException();
        }

        public T update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
