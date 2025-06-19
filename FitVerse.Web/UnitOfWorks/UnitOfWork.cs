using FitVerse.Web.Models;
using FitVerse.Web.Repositories;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        ProductRepository productRepository;
        FitVerseContext _context;
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
