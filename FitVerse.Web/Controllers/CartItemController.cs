using AspNetCoreGeneratedDocument;
using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace FitVerse.Web.Controllers
{
    public class CartItemController : Controller
    {
        IUnitOfWork _unit;
        IMapper _map;
        public CartItemController(IUnitOfWork unit, IMapper map)
        {
            _map = map;
            _unit = unit;
        }

        [Authorize]
         public IActionResult Index()
         {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.TotalCost = 0;
            List<CartItem> userCartItemsDB = _unit.CartItemRepository.GetUserCartItems(userId);
            List<CartItem_ViewModel> cartVM= _map.Map<List<CartItem_ViewModel>>(userCartItemsDB); // Mapping
            for (int i = 0; i < cartVM.Count; i++)
            {
                var product = _unit.ProductRepository.GetById(cartVM[i].Prod_Id);
                cartVM[i].Prod_Name = product.Name;
                cartVM[i].Price = product.Price;
                cartVM[i].ImageUrl= product.ImageUrl;
                cartVM[i].EffectivePrice = product.EffectivePrice * cartVM[i].Quantity;
                ViewBag.TotalCost += product.EffectivePrice * cartVM[i].Quantity ;
            }
            return View(cartVM);
         }

        [Authorize]
        public IActionResult Increment(int PId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(PId, userId);
            if (cartProduct == null)
            {
                TempData["ErrorMessage"] = "Product not found in cart";
                return RedirectToAction("Index");
            }
            //cartProduct.Quantity++;
            //_unit.CartItemRepository.Update(cartProduct);
            _unit.CartItemRepository.AddToCart(PId, userId);
            _unit.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddToCartAction(int PId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _unit.CartItemRepository.AddToCart(PId, userId);
            _unit.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Decrement(int PId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(PId, userId);
            //cartProduct.Quantity--;
            //_unit.CartItemRepository.Update(cartProduct);
            _unit.CartItemRepository.DecrementFromCart(PId, userId);
            _unit.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int PId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _unit.CartItemRepository.DeleteByProdId(PId, userId);
            _unit.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult DeleteAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _unit.CartItemRepository.RemoveAll(userId);
            _unit.Save();
            return RedirectToAction("Index");
        }
    }
}
