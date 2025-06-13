namespace FitVerse.Web.Repositories
{
    public interface IGenericRepo<T>
    {
        public T getall();
        public T find(int id);

        public T getbyname(string name);
        public void update(T entity);
        public void delete(int id);
    }
}
