using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        CartItemRepository cartItemRepository;
        ProductRepository productRepository;
        FitVerseContext _context;
        public UnitOfWork(FitVerseContext context)
        {
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
