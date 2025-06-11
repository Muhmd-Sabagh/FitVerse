using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
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
            List<Product> userProductsDB = _unit.ProductRepository.GetAll();
            //List<CartItem> userCartsDB = _unit.CartItemRepository.GetCartByUserId(userId);
            //CartItem userCartsDB = _unit.CartItemRepository.GetCartByUserId(1);

            //List<Product> userProductsDB = _unit.CartItemRepository.GetUserProducts();
            List<CartItem_ViewModel> cartVM= _map.Map<List<CartItem_ViewModel>>(userProductsDB); // Mapping
            //return View(userProductsVM);
            return View(cartVM);
            //return View();
        }
        public void Increment(int id)
        {
            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(id);
            cartProduct.Quantity++;
            _unit.CartItemRepository.Update(cartProduct);
            _unit.Save();
            RedirectToAction("Index");
        }
        public void Decrement(int id)
        {
            CartItem cartProduct = _unit.CartItemRepository.GetCartItemByProdId(id);
            cartProduct.Quantity--;
            _unit.CartItemRepository.Update(cartProduct);
            _unit.Save();
            RedirectToAction("Index");
        }
        public IActionResult Delete(int PId)
        {
            _unit.CartItemRepository.DeleteByProdId(PId);
            _unit.Save();
            return RedirectToAction("Index");
        }
    }
}
