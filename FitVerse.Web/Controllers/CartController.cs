using AutoMapper;
using E_commerce_Khotwa.Models;
using E_commerce_Khotwa.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Khotwa.Controllers
{
    public class CartController : Controller
    {
        UnitOfWork unit;
        IMapper map;
        public CartController(UnitOfWork _unit,IMapper _map)
        {
            unit = _unit;
            map = _map;
        }
        public IActionResult Index()
        {
            List<Product> userProductsDB = unit.CartRepository.GetUserProducts();
            //List<Product_ViewModel> userProductsVM = userProductsDB; // Mapping

            //return View(userProductsVM );
        }
    }
}
