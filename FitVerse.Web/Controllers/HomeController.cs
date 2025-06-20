using System.Diagnostics;
using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels;
using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Home;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UnitOfWork Unit { get; }
        public IMapper Mapper { get; }

        public HomeController(ILogger<HomeController> logger,UnitOfWork unit ,IMapper mapper)
        {
            _logger = logger;
            Unit = unit;
            Mapper = mapper;
        }
        
        public IActionResult Index()
        {
            var vm = new HomeViewModel();
           var products = Unit.ProductRepository.GetAll();
            vm.Products =Mapper.Map<List<ProductViewModel>>(products);
            vm.Categories = Mapper.Map<List<CategoryViewModel>>(Unit.CategoryRepo.getall());
            vm.Banners = Mapper.Map<List< BannarHomeViewModel >>(Unit.Banner.getall());

            //{
            //    //cat = Unit.
            //    //Categories = Unit.ProductRepository.GetAll(),
            //    //Categories = Unit.ProductRepository.GetAll(),
            //    //Products = Unit.ProductRepo.getall(),
            //    //Banners = Unit.Banner.getall(),


            //};
            return View(vm);
        }
        public IActionResult FilteredCategories(int genderId)

        {
            var products = Unit.ProductRepository.GetByParentCategoryId( genderId);
            return PartialView("_ProductListPartial",Mapper.Map<List<ProductViewModel>>(products));
        }
        public IActionResult NewArrival()
        {
            var NewArrivalProducts = Unit.ProductRepository.GetNewArrivalProducts();

            return PartialView("_NewArrivalPartial", NewArrivalProducts);
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
