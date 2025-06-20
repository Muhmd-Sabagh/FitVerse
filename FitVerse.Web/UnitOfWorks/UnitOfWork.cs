using FitVerse.Web.Models;
using FitVerse.Web.Repositories;
using FitVerse.Web.Repositories.Implementations;
using FitVerse.Web.Repositories.Interfaces;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        ProductRepository productRepository;
        CategoryRepository categoryRepository;
        FitVerseContext _context;
        public UnitOfWork(FitVerseContext context)
        {
            _context = context;
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

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(_context);
                return categoryRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
