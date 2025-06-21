using System;
using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>
    {
        FitVerseContext _context;
        public OrderRepository(FitVerseContext context) : base(context)
        {
            _context = context;
        }

        public List<string> GetAllProductsNamesfromOrder(int UId)
        {
            Order order = GetById(UId);
            List<OrderItem> orderItems = _context.OrderItems.Where(o=>o.Id == order.Id).ToList();
            List<string> productNames = new List<string>();
            foreach (OrderItem item in orderItems)
            {
                string productName = item.Product.Name;
                productNames.Add(productName);
            }
            return  productNames;
        }

        public List<Order> GetUserOrders(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

    }

    }

