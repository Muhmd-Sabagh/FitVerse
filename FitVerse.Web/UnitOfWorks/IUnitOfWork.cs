using FitVerse.Web.Interfaces;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;

namespace FitVerse.Web.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBannerRepository Banner { get; }
        CartItemRepository CartItemRepository { get; }
        OrderRepository Order { get; }
        void Save();
    }
}
