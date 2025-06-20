//using FitVerse.Web.Models;
//using FitVerse.Web.Repositories.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
//using Microsoft.EntityFrameworkCore; // For DbUpdateConcurrencyException
//using System.Threading.Tasks;

//namespace FitVerse.Web.Controllers
//{
//    [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access this controller
//    public class AdminProductsController : Controller
//    {
//        private readonly IProductRepository _productRepository;
//        private readonly ICategoryRepository _categoryRepository; // Assuming you have a category repository

//        public AdminProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
//        {
//            _productRepository = productRepository;
//            _categoryRepository = categoryRepository;
//        }

//        // GET: AdminProducts
//        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
//        {
//            if (pageNumber < 1) pageNumber = 1;
//            if (pageSize < 1) pageSize = 10;

//            (IEnumerable<Product> products, int totalProductsCount) = await _productRepository.GetPaginatedProductsAsync(pageNumber, pageSize);

//            ViewBag.CurrentPage = pageNumber;
//            ViewBag.PageSize = pageSize;
//            ViewBag.TotalItems = totalProductsCount;
//            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProductsCount / pageSize);
//            ViewBag.HasPreviousPage = pageNumber > 1;
//            ViewBag.HasNextPage = pageNumber < ViewBag.TotalPages;

//            return View(products);
//        }

//        // GET: AdminProducts/Details/5
//        public async Task<IActionResult> Details(int id)
//        {
//            var product = await _productRepository.GetProductByIdAsync(id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            return View(product);
//        }

//        // GET: AdminProducts/Create
//        public async Task<IActionResult> Create()
//        {
//            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
//            return View();
//        }

//        // POST: AdminProducts/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Name,Material,Description,Price,DiscountPercentage,IsNewArrival,IsActive,ImageUrl,StockQuantity,CategoryId")] Product product)
//        {
//            if (ModelState.IsValid)
//            {
//                await _productRepository.AddProductAsync(product);
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", product.CategoryId);
//            return View(product);
//        }

//        // GET: AdminProducts/Edit/5
//        public async Task<IActionResult> Edit(int id)
//        {
//            var product = await _productRepository.GetProductByIdAsync(id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", product.CategoryId);
//            return View(product);
//        }

//        // POST: AdminProducts/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Material,Description,Price,DiscountPercentage,IsNewArrival,IsActive,ImageUrl,StockQuantity,CategoryId,CreatedAt,UpdatedAt")] Product product)
//        {
//            if (id != product.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _productRepository.UpdateProductAsync(product);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (await _productRepository.GetProductByIdAsync(product.Id) == null)
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", product.CategoryId);
//            return View(product);
//        }

//        // GET: AdminProducts/Delete/5
//        public async Task<IActionResult> Delete(int id)
//        {
//            var product = await _productRepository.GetProductByIdAsync(id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            return View(product);
//        }

//        // POST: AdminProducts/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            await _productRepository.DeleteProductAsync(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
