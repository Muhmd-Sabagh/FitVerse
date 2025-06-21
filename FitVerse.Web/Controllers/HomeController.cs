using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Home;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitVerse.Web.Controllers
{
    // [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();

            var (products, totalProductsCount) = await _unitOfWork.Products.GetPaginatedAsync(1, 8);
            vm.Products = _mapper.Map<List<ProductViewModel>>(products.ToList());

            var categories = await _unitOfWork.Categories.GetAllAsync();
            vm.Categories = _mapper.Map<List<CategoryViewModel>>(categories.ToList());

            var banners = (await _unitOfWork.Banners.GetAllAsync()).Where(b => b.IsActive).ToList();
            vm.Banners = _mapper.Map<List<BannarHomeViewModel>>(banners);

            _logger.LogInformation("Home page accessed.");
            return View(vm);
        }

        // GET: Home/FilteredCategories?genderId=X
        public async Task<IActionResult> FilteredCategories(int genderId)
        {
            // First, find the category name from the ID
            var category = await _unitOfWork.Categories.GetByIdAsync(genderId);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {genderId} not found for filtering.");
                return PartialView("_ProductListPartial", new List<ProductViewModel>());
            }

            var products = await _unitOfWork.Products.GetByParentCategoryAsync(category.Name);
            _logger.LogInformation($"Filtered categories by gender ID: {genderId} ({category.Name}). Found {products?.Count() ?? 0} products.");
            return PartialView("_ProductListPartial", _mapper.Map<List<ProductViewModel>>(products.ToList()));
        }

        // GET: Home/NewArrival
        public async Task<IActionResult> NewArrival()
        {
            // Get NewArrival Products
            var newArrivalProducts = await _unitOfWork.Products.GetNewArrivalProductsAsync();

            _logger.LogInformation($"Accessed new arrival products. Found {newArrivalProducts?.Count() ?? 0} products.");
            return PartialView("_NewArrivalPartial", _mapper.Map<List<ProductViewModel>>(newArrivalProducts.ToList()));
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy policy page accessed.");
            return View();
        }

        // GET: Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError($"An error occurred. Request ID: {Activity.Current?.Id ?? HttpContext.TraceIdentifier}");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
