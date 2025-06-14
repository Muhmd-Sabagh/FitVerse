namespace FitVerse.Web.Repositories.Interfaces
{
    public interface IGenericRepo<T>
    {
        public List<T> getall();
        public T find(int id);

        //public T getbyname(string name);
        public T update(T entity);
        public T delete(int id);
    }
}
