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
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminProductsController> _logger;
        private const int DefaultPageSize = 10;

        public AdminProductsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AdminProductsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: AdminProducts
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            // Get paginated products
            var (products, totalCount) = await _unitOfWork.Products.GetPaginatedAsync(page, DefaultPageSize);

            _logger.LogInformation($"Admin accessed product index (Page: {page}). Total items: {products.Count()}, Total count in DB: {totalCount}.");
            return View(products);
        }

        // GET: AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get product by ID
            var product = await _unitOfWork.Products.GetByIdAsync(id.Value);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found for details (Admin).");
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductDetailsViewModel>(product);
            return View(viewModel);
        }

        // GET: AdminProducts/Create
        public async Task<IActionResult> Create()
        {
            // Get all categories
            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: AdminProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormAddData productFromReq)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productFromReq);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                // Add product
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation($"Product '{product.Name}' created successfully by admin.");
                return RedirectToAction(nameof(Index));
            }

            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productFromReq.CategoryId);
            _logger.LogWarning("Admin product creation failed due to invalid model state.");
            return View(productFromReq);
        }

        // GET: AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.Products.GetByIdAsync(id.Value);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found for editing (Admin).");
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductFormEditData>(product);

            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", viewModel.CategoryId);
            return View(viewModel);
        }

        // POST: AdminProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductFormEditData productFromReq)
        {
            if (id != productFromReq.Id)
            {
                _logger.LogError($"Product ID mismatch: route ID {id}, model ID {productFromReq.Id}.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productToUpdate = _mapper.Map<Product>(productFromReq);
                    productToUpdate.UpdatedAt = DateTime.UtcNow;
                    // Update the product
                    _unitOfWork.Products.Update(productToUpdate);
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation($"Product '{productToUpdate.Name}' (ID: {productToUpdate.Id}) updated successfully by admin.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"Concurrency error updating product with ID {productFromReq.Id}.");
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
                    _logger.LogError(ex, $"Error updating product with ID {productFromReq.Id}.");
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var categories = await _unitOfWork.Categories.GetAllAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name", productFromReq.CategoryId);
            _logger.LogWarning($"Admin product (ID: {productFromReq.Id}) edit failed due to invalid model state.");
            return View(productFromReq);
        }

        // GET: AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.Products.GetByIdAsync(id.Value);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found for deletion confirmation (Admin).");
                return NotFound();
            }
            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Get product by ID
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found for confirmed deletion (Admin).");
                return NotFound();
            }
            // Delete the product
            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Product '{product.Name}' (ID: {id}) deleted successfully by admin.");
            return RedirectToAction(nameof(Index));
        }
    }
}
