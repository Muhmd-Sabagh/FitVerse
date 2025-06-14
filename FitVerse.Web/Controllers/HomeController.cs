using System.Diagnostics;
using FitVerse.Web.Models;
using FitVerse.Web.unitofworks;
using FitVerse.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Unitofwork Unit { get; }

        public HomeController(ILogger<HomeController> logger,Unitofwork unit )
        {
            _logger = logger;
            Unit = unit;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel()
            {
                //cat = Unit.Category.find()
                Categories = Unit.CategoryRepo.getall(),
                Products = Unit.ProductRepo.getall(),
                Banners = Unit.Banner.getall(),

            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
