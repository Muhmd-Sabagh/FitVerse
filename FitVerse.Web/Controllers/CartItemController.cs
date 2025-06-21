using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemController> _logger;

        public CartItemController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CartItemController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // Helper to get current user ID
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: CartItem/Index
        public async Task<IActionResult> Index()
        {
            string currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId))
            {
                _logger.LogWarning("Attempted to access cart without a user ID. Redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            List<CartItem> userCartItemsDB = (await _unitOfWork.CartItems.GetUserCartItemsAsync(currentUserId)).ToList();

            List<CartItem_ViewModel> cartVM = new List<CartItem_ViewModel>();
            decimal totalCost = 0;

            foreach (var cartItem in userCartItemsDB)
            {
                if (cartItem.Product != null)
                {
                    var cartItemVm = _mapper.Map<CartItem_ViewModel>(cartItem);
                    cartItemVm.Prod_Name = cartItem.Product.Name;
                    cartItemVm.Price = cartItem.Product.Price;
                    cartItemVm.ImageUrl = cartItem.Product.ImageUrl;
                    cartItemVm.EffectivePrice = cartItem.Product.EffectivePrice;
                    cartItemVm.TotalPrice = cartItem.Product.EffectivePrice * cartItem.Quantity;
                    cartVM.Add(cartItemVm);
                    totalCost += cartItemVm.TotalPrice;
                }
                else
                {
                    _logger.LogWarning($"Product associated with cart item ID {cartItem.Id} for user {currentUserId} not found or inactive.");
                }
            }

            ViewBag.TotalCost = totalCost;
            _logger.LogInformation($"User {currentUserId} accessed cart. Total items: {cartVM.Count}, Total cost: {totalCost:C}.");
            return View(cartVM);
        }

        // GET: CartItem/Increment?PId=X
        public async Task<IActionResult> Increment(int PId)
        {
            string currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return RedirectToAction("Login", "Account");

            bool success = await _unitOfWork.CartItems.AddToCartAsync(currentUserId, PId);
            await _unitOfWork.CompleteAsync();

            if (!success)
            {
                TempData["ErrorMessage"] = "Could not increment quantity. Max stock reached or product not found.";
                _logger.LogWarning($"User {currentUserId} failed to increment product ID {PId} in cart (stock/not found).");
            }
            else
            {
                _logger.LogInformation($"User {currentUserId} incremented product ID {PId} in cart.");
            }

            return RedirectToAction("Index");
        }

        // GET: CartItem/Decrement?PId=X
        public async Task<IActionResult> Decrement(int PId)
        {
            string currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return RedirectToAction("Login", "Account");

            await _unitOfWork.CartItems.DecrementFromCartAsync(currentUserId, PId);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"User {currentUserId} decremented product ID {PId} in cart.");
            return RedirectToAction("Index");
        }

        // GET: CartItem/Delete?PId=X
        public async Task<IActionResult> Delete(int PId)
        {
            string currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return RedirectToAction("Login", "Account");

            await _unitOfWork.CartItems.DeleteByProdIdAsync(currentUserId, PId);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"User {currentUserId} deleted product ID {PId} from cart.");
            return RedirectToAction("Index");
        }

        // GET: CartItem/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            string currentUserId = GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId)) return RedirectToAction("Login", "Account");

            await _unitOfWork.CartItems.RemoveAllAsync(currentUserId);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"User {currentUserId} cleared all items from cart.");
            return RedirectToAction("Index");
        }
    }
}
