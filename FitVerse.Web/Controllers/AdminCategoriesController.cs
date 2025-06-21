using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdminCategoriesController> _logger;

        public AdminCategoriesController(IUnitOfWork unitOfWork, ILogger<AdminCategoriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: AdminCategories
        public async Task<IActionResult> Index()
        {
            // Get all categories
            var categories = await _unitOfWork.Categories.GetAllAsync();
            _logger.LogInformation("Admin accessed category index.");
            return View(categories);
        }

        // GET: AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get category by ID
            var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found for details.");
                return NotFound();
            }
            return View(category);
        }

        // GET: AdminCategories/Create
        public async Task<IActionResult> Create()
        {
            // Get parent categories
            var parentCategories = await _unitOfWork.Categories.GetParentCategoriesAsync();
            ViewBag.ParentCategoryList = new SelectList(parentCategories, "Id", "Name");
            return View();
        }

        // POST: AdminCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ImageUrl,ParentCategoryId,IsActive")] Category category)
        {
            if (category.ParentCategoryId == null) ModelState.Remove("ParentCategoryId");

            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.UtcNow;
                category.UpdatedAt = DateTime.UtcNow;
                // Add category
                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation($"Category '{category.Name}' created successfully.");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("Category creation failed due to invalid model state.");
            var parentCategories = await _unitOfWork.Categories.GetParentCategoriesAsync();
            ViewBag.ParentCategoryList = new SelectList(parentCategories, "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // GET: AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get category by ID
            var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found for editing.");
                return NotFound();
            }

            // Get all categories
            var allCategories = await _unitOfWork.Categories.GetAllAsync();
            var nonDescendantCategories = allCategories.Where(c => c.Id != id.Value && !IsDescendant(c, category, allCategories)).ToList();
            ViewBag.ParentCategoryList = new SelectList(nonDescendantCategories, "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        private bool IsDescendant(Category potentialDescendant, Category parent, IEnumerable<Category> allCategories)
        {
            if (potentialDescendant == null || parent == null || potentialDescendant.Id == parent.Id) return false;
            var currentCategory = potentialDescendant;
            while (currentCategory != null && currentCategory.ParentCategoryId.HasValue)
            {
                if (currentCategory.ParentCategoryId.Value == parent.Id) return true;
                currentCategory = allCategories.FirstOrDefault(c => c.Id == currentCategory.ParentCategoryId.Value);
            }
            return false;
        }

        // POST: AdminCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,ParentCategoryId,IsActive,CreatedAt")] Category category)
        {
            if (id != category.Id)
            {
                _logger.LogError($"Category ID mismatch: route ID {id}, model ID {category.Id}.");
                return NotFound();
            }
            if (category.ParentCategoryId == null) ModelState.Remove("ParentCategoryId");

            if (ModelState.IsValid)
            {
                try
                {
                    category.UpdatedAt = DateTime.UtcNow;
                    // Update the category
                    _unitOfWork.Categories.Update(category);
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation($"Category '{category.Name}' (ID: {category.Id}) updated successfully.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"Concurrency error updating category with ID {category.Id}.");
                    // Check if the category still exists before throwing
                    if (await _unitOfWork.Categories.GetByIdAsync(category.Id) == null)
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
                    _logger.LogError(ex, $"Error updating category with ID {category.Id}.");
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var allCategories = await _unitOfWork.Categories.GetAllAsync();
            var nonDescendantCategories = allCategories.Where(c => c.Id != id && !IsDescendant(c, category, allCategories)).ToList();
            ViewBag.ParentCategoryList = new SelectList(nonDescendantCategories, "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // GET: AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Get category by ID
            var category = await _unitOfWork.Categories.GetByIdAsync(id.Value);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {id} not found for deletion confirmation.");
                return NotFound();
            }
            return View(category);
        }

        // POST: AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetByIdAsync(id);
            if (categoryToDelete == null)
            {
                _logger.LogWarning($"Category with ID {id} not found for confirmed deletion.");
                return NotFound();
            }

            // Check for associated products
            var productsInCategory = await _unitOfWork.Products.GetByCategoryAsync(categoryToDelete.Name);
            if (productsInCategory.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete category as there are products associated with it. Please reassign or delete products first.");
                _logger.LogWarning($"Attempted to delete category (ID: {id}) with associated products.");
                return View(categoryToDelete);
            }

            var categoryWithChildren = await _unitOfWork.Categories.GetCategoryWithSubcategoriesAsync(id);
            if (categoryWithChildren != null && categoryWithChildren.SubCategories != null && categoryWithChildren.SubCategories.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete category as it has subcategories. Please delete subcategories first.");
                _logger.LogWarning($"Attempted to delete category (ID: {id}) with subcategories.");
                return View(categoryToDelete);
            }

            // Delete the category
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Category '{categoryToDelete.Name}' (ID: {id}) deleted successfully.");
            return RedirectToAction(nameof(Index));
        }
    }
}
