using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace FitVerse.Web.Controllers
{
    public class CheckoutController : Controller
    {
        UnitOfWork _unit;
        IMapper _map;
        public CheckoutController(UnitOfWork unit, IMapper map)
        {
            _map = map;
            _unit = unit;
        }
        public IActionResult Index()
        {
            Checkout_ViewModel checkout_ViewModel = new Checkout_ViewModel();
            List<CartItem> cartItems = _unit.CartItemRepository.GetUserCartItems();
            checkout_ViewModel.CartItems.AddRange(cartItems);
            return View(checkout_ViewModel);
        }
        [HttpPost]
        public IActionResult SaveOrder(Checkout_ViewModel checkoutVM) {
            List<OrderItem> orderItems=new List<OrderItem>();
            Order order = new Order();
            order.ShippingAddress = checkoutVM.ShippingAddress;
            order.CustomerPhone = checkoutVM.CustomerPhone;
            order.CustomerEmail = checkoutVM.CustomerEmail;
            order.CustomerName = checkoutVM.CustomerName;
            order.OrderDate = checkoutVM.OrderDate;
            order.OrderItems = orderItems;
            _unit.Save();
            foreach (CartItem cartItem in checkoutVM.CartItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.UnitPrice = cartItem.Product.EffectivePrice;
                orderItem.Quantity = cartItem.Quantity;
                orderItem.OrderId = order.Id;
                _unit.OrderItem.Add(orderItem);
            }
            _unit.Save();
            List<Order> orders = _unit.Order.GetAll();

            return View("MyOrders", orders);
            //return View("MyOrders");  
        }
    }
}
