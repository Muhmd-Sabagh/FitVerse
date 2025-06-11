using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class CartItemController : Controller
    {
        UnitOfWork _unit;
        //IMapper _map;
        public CartItemController(UnitOfWork unit)
        {
            //_map = map;
            _unit = unit;
        }
        public IActionResult Index()
        {
            //List<Product> userProductsDB = _unit.CartItemRepository.GetUserProducts();
            //List<Product_ViewModel> userProductsVM = map.Map<List<Product_ViewModel>>(userProductsDB); // Mapping
            List<Product> userProductsDB = _unit.ProductRepository.GetAll();
            //return View(userProductsVM);
            return View(userProductsDB);
            //return View();
        }
    }
}
