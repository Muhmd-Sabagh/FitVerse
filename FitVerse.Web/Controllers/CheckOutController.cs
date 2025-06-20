using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Cart;
using FitVerse.Web.ViewModels.Checkout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    public class CheckoutController : Controller
    {
        string userId = "1";
        UnitOfWork _unit;
        IMapper _map;
        FitVerseContext _context;
        public CheckoutController(UnitOfWork unit, IMapper map, FitVerseContext context)
        {
            _context = context;
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
        public IActionResult SaveOrder([FromBody] Checkout_ViewModel checkoutVM)
        {
            List<CartItem> cartItems = _unit.CartItemRepository.GetUserCartItems();
            List<OrderItem> orderItems = _map.Map<List<OrderItem>>(cartItems);

            var totalPricefromCartItems = _unit.CartItemRepository.getCartCost();


            Order order = new Order();
            order.ShippingAddress = checkoutVM.ShippingAddress;
            order.CustomerPhone = checkoutVM.CustomerPhone;
            order.CustomerEmail = checkoutVM.CustomerEmail;
            order.CustomerName = checkoutVM.CustomerName;
            order.OrderDate = checkoutVM.OrderDate;
            order.OrderItems = orderItems;
            order.UserId = userId;
            order.TotalAmount = totalPricefromCartItems;
            _unit.Order.Add(order);
            _unit.Save();
            Order_ViewModel orderVM = _map.Map<Order_ViewModel>(order);
            return Ok(new
            {
                success = true,
                redirectUrl = Url.Action("OrderDetails", new { id = order.Id })
            });
        }
        public async Task<IActionResult> OrderDetails(int id)
        {
            Order order = await _context.Orders.Where(o => o.Id == id)
                .Include(o => o.OrderItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync();

            var x = order.OrderItems.FirstOrDefault().Product.EffectivePrice;
            var y = order.OrderItems.First().Product.EffectivePrice;

            if (order != null)
                return View("MyOrderDetails", order);
            else return RedirectToAction("Index");
        }
        public IActionResult AllOrders()
        {
            List<Order> orders = _unit.Order.GetAll();
            if (orders == null) RedirectToAction("Index");
            return View("AllOrdersView", orders);
        }
    }
}
