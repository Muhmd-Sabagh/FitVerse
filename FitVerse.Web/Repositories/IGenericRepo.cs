namespace FitVerse.Web.Repositories
{
    public interface IGenericRepo<T>
    {
        public T getall();
        public T find(int id);

        public T getbyname(string name);
        public T update(T entity);
        public T delete(int id);
    }
}
