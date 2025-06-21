using FitVerse.Web.Interfaces;
using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;

namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        FitVerseContext _context;
        CartItemRepository cartItemRepository;
        ProductRepository productRepository;
        CategoryRepository categoryRepository;
        OrderItemRepository orderItemRepository;
        OrderRepository order;
        BannerRepository banner;

        public UnitOfWork(FitVerseContext context)
        {
            _context = context;
        }
        public CartItemRepository CartItemRepository
        {
            get
            {
                if (cartItemRepository == null)
                    cartItemRepository = new CartItemRepository(_context);
                return cartItemRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(_context);
                return productRepository;
            }
        }
        public OrderItemRepository OrderItem
        {
            get
            {
                if (orderItemRepository == null)
                    orderItemRepository = new OrderItemRepository(_context);
                return orderItemRepository;
            }
        }
        public OrderRepository Order
        {
            get
            {
                if (order == null)
                    order = new OrderRepository(_context);
                return order;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(_context);
                return categoryRepository;
            }
        }

        public IBannerRepository Banner
        {
            get
            {
                if (banner == null)
                    banner = new BannerRepository(_context);
                return banner;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
