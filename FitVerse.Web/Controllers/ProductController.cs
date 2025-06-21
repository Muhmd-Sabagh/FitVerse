using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
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
            ViewBag.page = page;
            return View("All", productsVM);

        }

        public IActionResult Details(int id)
        {
            Product product = unitOfWork.ProductRepository.GetById(id);
            ProductDetailsViewModel prodDetailsVM = map.Map<ProductDetailsViewModel>(product);
            return View("Details", prodDetailsVM);

        }

        public IActionResult Category(string category, int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.GetByCategory(page, category);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            ViewBag.page = page;
            return View("All", productsVM);

        }

        public IActionResult ParentCategory(string parentCategory, string category = "", int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.GetByParentCategory(parentCategory, page, category);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            ViewBag.page = page;
            return View("All", productsVM);

        }

        public IActionResult GetNew(int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.GetNewArrivalProducts(page);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            ViewBag.page = page;
            return View("All", productsVM);
        }

        public IActionResult Search(string name, string category = "", int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.SearchByName(page, name, category);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            ViewBag.page = page;
            return View("All", productsVM);

        }
        public IActionResult Filter(string name, string parentCategory = "", string category = "", int price = 0, int page = 1)
        {
            List<Product> products = unitOfWork.ProductRepository.Filter(page, price, parentCategory, category, name);
            List<ProductCardViewModel> productsVM = map.Map<List<ProductCardViewModel>>(products);
            ViewBag.page = page;
            return View("All", productsVM);

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewBag.ParentCategoryList = unitOfWork.CategoryRepository.GetParentCategories();
            return View("Add");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(ProductFormAddData productFromReq)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = map.Map<Product>(productFromReq);
                unitOfWork.ProductRepository.Add(newProduct);
                unitOfWork.Save();
                return RedirectToAction("All", "Product");
            }
            ViewBag.ParentCategoryList = unitOfWork.CategoryRepository.GetParentCategories();
            return View("Add", productFromReq);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Product productFromDB = unitOfWork.ProductRepository.GetById(id);
            ProductFormEditData productToForm = map.Map<ProductFormEditData>(productFromDB);
            ViewBag.ParentCategoryList = unitOfWork.CategoryRepository.GetParentCategories();
            return View("Edit", productToForm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductFormEditData productFromReq)
        {
            if (ModelState.IsValid)
            {
                Product EdittedProduct = map.Map<Product>(productFromReq);
                unitOfWork.ProductRepository.Edit(EdittedProduct);
                unitOfWork.Save();
                return RedirectToAction("All", "Product");
            }
            ViewBag.ParentCategoryList = unitOfWork.CategoryRepository.GetParentCategories();
            return View("Edit", productFromReq);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            unitOfWork.ProductRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("All");

        }
    }
}
