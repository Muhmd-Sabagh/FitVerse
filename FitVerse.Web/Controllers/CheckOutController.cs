using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Cart;
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
            List<CartItem_ViewModel> cartItemVMs = _map.Map<List<CartItem_ViewModel>>(cartItems);
            //checkout_ViewModel.CartItemsViewModel.AddRange(cartItems);
            return View(checkout_ViewModel);
        }
        [HttpPost]
        public IActionResult SaveOrder([FromBody]Checkout_ViewModel checkoutVM) {
            List<CartItem>cartItems = _unit.CartItemRepository.GetUserCartItems();
            List<OrderItem> orderItems = _map.Map<List<OrderItem>>(cartItems);
            
            Order order = new Order();
            order.ShippingAddress = checkoutVM.ShippingAddress;
            order.CustomerPhone = checkoutVM.CustomerPhone;
            order.CustomerEmail = checkoutVM.CustomerEmail;
            order.CustomerName = checkoutVM.CustomerName;
            order.OrderDate = checkoutVM.OrderDate;
            order.OrderItems = orderItems;
            _unit.Save();
            List<Order> orders = _unit.Order.GetAll();

            return View("MyOrders", orders);
        }
    }
}
