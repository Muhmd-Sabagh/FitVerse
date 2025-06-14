using FitVerse.Web.Models;
using FitVerse.Web.Repositories;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        CartItemRepository cartItemRepository;
        IProductRepository productRepository;
        FitVerseContext _context;
        public UnitOfWork(CartItemRepository cartItemRepo,FitVerseContext context, IProductRepository productRepo)
        {
            cartItemRepository = cartItemRepo;
            _context = context;
            productRepository = productRepo;
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
        public IProductRepository ProductRepository
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
