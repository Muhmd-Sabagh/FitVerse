using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks; // For IUnitOfWork
using FitVerse.Web.ViewModels.Cart;
using FitVerse.Web.ViewModels.Checkout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CheckoutController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // Get current user ID
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: Checkout/Index
        public async Task<IActionResult> Index()
        {
            string userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempted to access checkout without a user ID. Redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            List<CartItem> cartItems = (await _unitOfWork.CartItems.GetUserCartItemsAsync(userId)).ToList();

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty. Please add items before checking out.";
                _logger.LogInformation($"User {userId} attempted checkout with an empty cart.");
                return RedirectToAction("Index", "CartItem");
            }

            List<CartItem_ViewModel> cartItemVMs = new List<CartItem_ViewModel>();
            decimal totalCost = 0;
            foreach (var cartItem in cartItems)
            {
                if (cartItem.Product != null)
                {
                    var cartItemVm = _mapper.Map<CartItem_ViewModel>(cartItem);
                    cartItemVm.Prod_Name = cartItem.Product.Name;
                    cartItemVm.Price = cartItem.Product.Price;
                    cartItemVm.ImageUrl = cartItem.Product.ImageUrl;
                    cartItemVm.DiscountPercentage = cartItem.Product.DiscountPercentage;
                    cartItemVm.EffectivePrice = cartItem.Product.EffectivePrice;
                    cartItemVm.TotalPrice = cartItem.Product.EffectivePrice * cartItem.Quantity;
                    cartItemVMs.Add(cartItemVm);
                    totalCost += cartItemVm.TotalPrice;
                }
                else
                {
                    _logger.LogWarning($"Product ID {cartItem.ProductId} in cart for user {userId} not found during checkout. Item will be ignored.");
                }
            }

            Checkout_ViewModel checkout_ViewModel = new Checkout_ViewModel
            {
                CartItemsViewModels = cartItemVMs,
                CustomerName = User.Identity.Name,
                CustomerEmail = User.FindFirstValue(ClaimTypes.Email),
            };

            ViewBag.TotalCost = totalCost;
            _logger.LogInformation($"User {userId} accessed checkout. Cart total: {totalCost:C}.");
            return View(checkout_ViewModel);
        }

        // POST: Checkout/SaveOrder
        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] Checkout_ViewModel checkoutVM)
        {
            string userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempted to save order without a user ID.");
                return Unauthorized(new { success = false, message = "User not authenticated." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning($"Checkout model state invalid for user {userId}: {string.Join("; ", errors)}");
                return BadRequest(new { success = false, message = "Invalid data provided. Please check your shipping details.", errors = errors });
            }

            List<CartItem> cartItems = (await _unitOfWork.CartItems.GetUserCartItemsAsync(userId)).ToList();

            if (!cartItems.Any())
            {
                _logger.LogWarning($"User {userId} attempted to save an empty order.");
                return BadRequest(new { success = false, message = "Your cart is empty. Cannot place an empty order." });
            }

            decimal totalPricefromCartItems = 0;
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                if (product == null || product.StockQuantity < cartItem.Quantity)
                {
                    _logger.LogWarning($"User {userId}: Product ID {cartItem.ProductId} unavailable or insufficient stock during order save. Available: {product?.StockQuantity ?? 0}, Requested: {cartItem.Quantity}");
                    return BadRequest(new { success = false, message = $"Product '{product?.Name ?? "Unknown"}' is out of stock or requested quantity is not available. Please review your cart." });
                }

                OrderItem orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = product.EffectivePrice,
                    CreatedAt = DateTime.UtcNow
                };
                orderItems.Add(orderItem);
                totalPricefromCartItems += orderItem.Quantity * orderItem.UnitPrice;

                product.StockQuantity -= cartItem.Quantity;
                _unitOfWork.Products.Update(product);
            }

            Order order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                CustomerPhone = checkoutVM.CustomerPhone,
                CustomerEmail = checkoutVM.CustomerEmail,
                CustomerName = checkoutVM.CustomerName,
                ShippingAddress = checkoutVM.ShippingAddress,
                Status = "Pending",
                TotalAmount = totalPricefromCartItems,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderItems = orderItems
            };

            // Add order and remove cart items using
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CartItems.RemoveAllAsync(userId);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Order ID {order.Id} placed successfully by user {userId}. Total: {order.TotalAmount:C}");
            return Ok(new
            {
                success = true,
                redirectUrl = Url.Action("OrderDetails", new { id = order.Id })
            });
        }

        // GET: Checkout/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int id)
        {
            string userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            Order order = await _unitOfWork.Orders.GetOrderWithItemsAndProductsAsync(id);

            if (order == null || order.UserId != userId)
            {
                _logger.LogWarning($"User {userId} attempted to access non-existent or unauthorized order ID: {id}.");
                return NotFound();
            }

            _logger.LogInformation($"User {userId} viewed details for order ID: {id}.");
            return View("MyOrderDetails", order);
        }

        // GET: Checkout/AllOrders
        public async Task<IActionResult> AllOrders()
        {
            string userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempted to access all orders without a user ID. Redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            var orders = (await _unitOfWork.Orders.GetAllAsync())
                                        .Where(o => o.UserId == userId)
                                        .OrderByDescending(o => o.OrderDate)
                                        .ToList();

            if (!orders.Any())
            {
                _logger.LogInformation($"User {userId} has no orders found.");
            }
            else
            {
                _logger.LogInformation($"User {userId} viewed all orders. Found {orders.Count} orders.");
            }

            return View("AllOrdersView", orders);
        }
    }
}
