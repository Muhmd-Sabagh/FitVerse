using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork unitOfWork;
        IMapper map;
        public ProductController(IUnitOfWork _unitOfWork, IMapper _map)
        {
            unitOfWork = _unitOfWork;
            map = _map;
        }
        public IActionResult All(int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.GetAll(page);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            return View("All", productsVM);

        }

        public IActionResult Details(int id)
        {
            Product product = unitOfWork.ProductRepository.GetById(id);
            ProductDetailsViewModel prodDetailsVM = map.Map<ProductDetailsViewModel>(product);
            return View("Details", prodDetailsVM);

        }


        public IActionResult Delete(int id)
        {
            unitOfWork.ProductRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("All");

        }
    }
}
