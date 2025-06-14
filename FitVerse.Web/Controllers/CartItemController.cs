using AspNetCoreGeneratedDocument;
using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;


namespace FitVerse.Web.Controllers
{
    public class CartItemController : Controller
    {
        UnitOfWork _unit;
        IMapper _map;
        public CartItemController(UnitOfWork unit, IMapper map)
        {
            _map = map;
            _unit = unit;
        }
        

         public IActionResult Index()
         {
            ViewBag.TotalCost = 0;
            List<Product> userProductsDB = _unit.ProductRepository.GetAll();
            List<CartItem> userCartItemsDB = _unit.CartItemRepository.GetUserCart();
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
        public IActionResult Increment(int PId)
        {
            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(PId);
            if (cartProduct == null)
            {
                TempData["ErrorMessage"] = "Product not found in cart";
                return RedirectToAction("Index");
            }
            cartProduct.Quantity++;
            _unit.CartItemRepository.Update(cartProduct);
            _unit.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Decrement(int PId)
        {
            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(PId);
            cartProduct.Quantity--;
            _unit.CartItemRepository.Update(cartProduct);
            _unit.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int PId)
        {
            _unit.CartItemRepository.DeleteByProdId(PId);
            _unit.Save();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteAll()
        {
            _unit.CartItemRepository.RemoveAll();
            _unit.Save();
            return RedirectToAction("Index");
        }
    }
}
