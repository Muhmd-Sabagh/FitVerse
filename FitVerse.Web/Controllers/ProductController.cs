using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using FitVerse.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private const int DefaultPageSize = 10;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Product/All
        public async Task<IActionResult> All(int page = 1)
        {
            if (page < 1) page = 1;

            var (products, totalCount) = await _unitOfWork.Products.GetPaginatedAsync(page, DefaultPageSize);
            List<ProductCardViewModel> productsVM = _mapper.Map<List<ProductCardViewModel>>(products.ToList());

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / DefaultPageSize);
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < ViewBag.TotalPages;

            _logger.LogInformation($"Accessed all products (Page: {page}). Total items on page: {productsVM.Count}, Total count in DB: {totalCount}.");
            return View("All", productsVM);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null || !product.IsActive)
            {
                _logger.LogWarning($"Product with ID {id} not found or not active for details.");
                return NotFound();
            }

            ProductDetailsViewModel prodDetailsVM = _mapper.Map<ProductDetailsViewModel>(product);

            _logger.LogInformation($"Accessed details for product ID: {id}, Name: {product.Name}.");
            return View("Details", prodDetailsVM);
        }

        // GET: Product/Category?category=CategoryName&page=1
        public async Task<IActionResult> Category(string category, int page = 1)
        {
            if (page < 1) page = 1;

            var products = await _unitOfWork.Products.GetByCategoryAsync(category, page, DefaultPageSize);
            int totalCount = (await _unitOfWork.Products.FilterAsync(categoryName: category)).Count();

            List<ProductCardViewModel> productsVM = _mapper.Map<List<ProductCardViewModel>>(products.ToList());

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / DefaultPageSize);
            ViewBag.HasPreviousPage = page > 1;
            ViewBag.HasNextPage = page < ViewBag.TotalPages;

            _logger.LogInformation($"Accessed products in category '{category}' (Page: {page}). Total items on page: {productsVM.Count}, Total count in DB: {totalCount}.");
            return View("All", productsVM);
        }

        // Admin-only CRUD actions
        // GET: Product/Add
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            _logger.LogInformation("Admin accessed product add form (via ProductController).");
            return View("Create");
        }

        // POST: Product/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ProductFormAddData productFromReq)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = _mapper.Map<Product>(productFromReq);
                newProduct.CreatedAt = DateTime.UtcNow;
                newProduct.UpdatedAt = DateTime.UtcNow;
                newProduct.IsActive = true;

                await _unitOfWork.Products.AddAsync(newProduct);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation($"Product '{newProduct.Name}' added successfully by admin (from ProductController).");
                return RedirectToAction("All", "Product");
            }

            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productFromReq.CategoryId);
            _logger.LogWarning("Admin product add failed due to invalid model state (from ProductController).");
            return View("Create", productFromReq);
        }

        // GET: Product/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Product productFromDB = await _unitOfWork.Products.GetByIdAsync(id);
            if (productFromDB == null)
            {
                _logger.LogWarning($"Product with ID {id} not found for edit (via ProductController).");
                return NotFound();
            }

            ProductFormEditData productToForm = _mapper.Map<ProductFormEditData>(productFromDB);

            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productToForm.CategoryId);
            _logger.LogInformation($"Admin accessed product edit form for ID: {id} (via ProductController).");
            return View("Edit", productToForm);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ProductFormEditData productFromReq)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingProduct = await _unitOfWork.Products.GetByIdAsync(productFromReq.Id);
                    if (existingProduct == null)
                    {
                        _logger.LogWarning($"Product with ID {productFromReq.Id} not found for update (concurrency check failed or deleted).");
                        return NotFound();
                    }

                    existingProduct.Name = productFromReq.Name;
                    existingProduct.Material = productFromReq.Material;
                    existingProduct.Description = productFromReq.Description;
                    existingProduct.Price = productFromReq.Price;
                    existingProduct.DiscountPercentage = productFromReq.DiscountPercentage;
                    existingProduct.IsNewArrival = productFromReq.IsNewArrival;
                    existingProduct.IsActive = productFromReq.IsActive;
                    existingProduct.ImageUrl = productFromReq.ImageUrl;
                    existingProduct.StockQuantity = productFromReq.StockQuantity;
                    existingProduct.CategoryId = productFromReq.CategoryId;
                    existingProduct.UpdatedAt = DateTime.UtcNow;

                    _unitOfWork.Products.Update(existingProduct);
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation($"Product '{existingProduct.Name}' (ID: {existingProduct.Id}) updated successfully by admin (via ProductController).");
                    return RedirectToAction("All", "Product");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"Concurrency error updating product with ID {productFromReq.Id} (via ProductController).");
                    // Check if the product still exists before throwing
                    if (await _unitOfWork.Products.GetByIdAsync(productFromReq.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error updating product with ID {productFromReq.Id} (via ProductController).");
                    throw;
                }
            }
            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productFromReq.CategoryId);
            _logger.LogWarning($"Admin product (ID: {productFromReq.Id}) edit failed due to invalid model state (via ProductController).");
            return View("Edit", productFromReq);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Product ID: {id} deleted successfully by admin (via ProductController).");
            return RedirectToAction("All");
        }
    }
}
