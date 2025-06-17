using System.Reflection.Metadata.Ecma335;
using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Implementations;
namespace FitVerse.Web.UnitOfWorks
{
    public class UnitOfWork
    {
        FitVerseContext _context;
        CartItemRepository cartItemRepository;
        ProductRepository productRepository;
        OrderItemRepository orderItemRepository;
        OrderRepository order;
     
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
        public OrderItemRepository OrderItem { 
            get {
                if (orderItemRepository == null)
                    orderItemRepository = new OrderItemRepository(_context);
                return orderItemRepository;
            } 
        }
        public OrderRepository Order
        {
            get {
                if (order == null)
                    order = new OrderRepository(_context);
                return order;
            } 
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
