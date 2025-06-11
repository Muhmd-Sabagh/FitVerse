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
            List<Product_ViewModel> userProductsVM = _map.Map<List<Product_ViewModel>>(userProductsDB); // Mapping
            //List<Product> userProductsDB = _unit.ProductRepository.GetAll();
            //return View(userProductsVM);
            return View(userProductsVM);
            //return View();
        }
    }
}
