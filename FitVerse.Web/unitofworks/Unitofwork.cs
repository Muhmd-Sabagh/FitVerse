using FitVerse.Web.Models;
using FitVerse.Web.Repositories;
using FitVerse.Web.Repositories.Implementations;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        ICartItemRepository cartItemRepository;
        ProductRepository productRepository;
        FitVerseContext _context;
        CategoryRepo categoryRepo;
        GenericRepo<Banner> bannerRepo;
        public UnitOfWork(FitVerseContext context)
        {
            //cartItemRepository = cartItemRepo;
            _context = context;
            //ProductRepository = productRepo;
        }
        //public CartItemRepository CartItemRepository
        //{
        //    get
        //    {
        //        if (cartItemRepository== null)
        //            cartItemRepository = new CartItemRepository(_context);
        //        return cartItemRepository;
        //    }
        //}
        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(_context);
                return productRepository;
            }
        }
        public GenericRepo<Banner> Banner
        {
            get
            {
                if (bannerRepo == null)
                    bannerRepo = new GenericRepo<Banner>(_context);
                return bannerRepo;
            }
        }
        public CategoryRepo CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                {
                    categoryRepo = new CategoryRepo(_context);
                }
                return categoryRepo;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
