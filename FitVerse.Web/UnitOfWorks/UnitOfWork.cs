using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;

namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitVerseContext _context;

        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;
        private IBannerRepository? _bannerRepository;
        private ICartItemRepository? _cartItemRepository;
        private IOrderRepository? _orderRepository;
        private IOrderItemRepository? _orderItemRepository;

        public UnitOfWork(FitVerseContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
        public IBannerRepository Banners => _bannerRepository ??= new BannerRepository(_context);
        public ICartItemRepository CartItems => _cartItemRepository ??= new CartItemRepository(_context);
        public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);
        public IOrderItemRepository OrderItems => _orderItemRepository ??= new OrderItemRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
