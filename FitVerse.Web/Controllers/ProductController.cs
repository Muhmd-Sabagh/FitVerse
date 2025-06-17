using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork;
        IMapper map;
        public ProductController(UnitOfWork _unitOfWork, IMapper _map)
        {
            unitOfWork = _unitOfWork;
            map = _map;
        }
        public IActionResult AllProducts(int pageNumer = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.GetAll(pageNumer);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            return Content(productsVM.ToString());

        }

        public IActionResult ProductDetails(int id)
        {
            Product product = unitOfWork.ProductRepository.GetById(id);
            ProductDetailsViewModel prodDetailsVM = map.Map<ProductDetailsViewModel>(product);
            return Content(prodDetailsVM.ToString());

        }
    }
}
