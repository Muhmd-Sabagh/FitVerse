using FitVerse.Web.Repositories.Interfaces;

namespace FitVerse.Web.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        public void Save();
    }
}
