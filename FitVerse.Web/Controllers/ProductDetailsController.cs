//using FitVerse.Web.Repositories.Implementations;
//using FitVerse.Web.Repositories.Interfaces;
//using FitVerse.Web.UnitOfWorks;
//using FitVerse.Web.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace FitVerse.Web.Controllers
//{
//    public class ProductDetailsController : Controller
//    {
//        UnitOfWork Unit;
//        public ProductDetailsController(UnitOfWork _Unit)
//        {
//            Unit = _Unit;
//        }


//        public IActionResult Details(int id)
//        {
//            var product = Unit.ProductRepository.GetById(id);

//            if (product == null)
//            {
//                return NotFound();
//            }

//            string parentCategory = _detailsRepository.GetParentCategoryByChildId(id);
//            var viewModel = new DetailsViewModel
//            {
//                Id = product.Id,
//                Name = product.Name,
//                Material = product.Material,
//                Description = product.Description,
//                Price = product.Price,
//                DiscountPercentage = product.DiscountPercentage,
//                StockQuantity = product.StockQuantity,
//                ImageUrl = product.ImageUrl,
//                IsNewArrival = product.IsNewArrival,
//                CategoryName = product.Category.Name,
//                EffectivePrice = (int)product.EffectivePrice,
//                IsOnSale = product.IsOnSale,
//                ParentCategory = parentCategory


//            };
//            return View("Details",viewModel);
//        }
//    }
//}
