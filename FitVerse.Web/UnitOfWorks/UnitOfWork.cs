using FitVerse.Web.Models;
using FitVerse.Web.Repositories;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        CartItemRepository cartItemRepository;
        ProductRepository productRepository;
        FitVerseContext _context;
        public UnitOfWork(CartItemRepository cartItemRepo,FitVerseContext context)
        {
            cartItemRepository = cartItemRepo;
            _context = context;
        }
        public CartItemRepository CartItemRepository
        {
            get
            {
                if (cartItemRepository== null)
                    cartItemRepository = new CartItemRepository(_context);
                return cartItemRepository;
            }
        }
        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(_context);
                return productRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
