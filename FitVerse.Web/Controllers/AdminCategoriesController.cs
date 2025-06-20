using FitVerse.Web.Interfaces;
using FitVerse.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public AdminCategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: AdminCategories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAllCategoriesAsync());
        }

        // GET: AdminCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: AdminCategories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        // POST: AdminCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ImageUrl,IsActive,ParentCategoryId")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // GET: AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // POST: AdminCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,IsActive,ParentCategoryId,CreatedAt,UpdatedAt")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.UpdateCategoryAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _categoryRepository.GetCategoryByIdAsync(category.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(await _categoryRepository.GetAllCategoriesAsync(), "Id", "Name", category.ParentCategoryId);
            return View(category);
        }

        // GET: AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
