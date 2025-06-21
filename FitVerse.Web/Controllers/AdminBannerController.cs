using FitVerse.Web.Models;
using FitVerse.Web.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBannersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdminBannersController> _logger;

        public AdminBannersController(IUnitOfWork unitOfWork, ILogger<AdminBannersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: AdminBanners
        public async Task<IActionResult> Index()
        {
            var banners = await _unitOfWork.Banners.GetAllAsync();
            _logger.LogInformation("Admin accessed banner index.");
            return View(banners);
        }

        // GET: AdminBanners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _unitOfWork.Banners.GetByIdAsync(id.Value);
            if (banner == null)
            {
                _logger.LogWarning($"Banner with ID {id} not found for details.");
                return NotFound();
            }
            return View(banner);
        }

        // GET: AdminBanners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBanners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ImageUrl,IsActive")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                banner.CreatedAt = DateTime.UtcNow;
                banner.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Banners.AddAsync(banner);
                await _unitOfWork.CompleteAsync(); // Use CompleteAsync for saving changes
                _logger.LogInformation($"Banner '{banner.Title}' created successfully.");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("Banner creation failed due to invalid model state.");
            return View(banner);
        }

        // GET: AdminBanners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _unitOfWork.Banners.GetByIdAsync(id.Value);
            if (banner == null)
            {
                _logger.LogWarning($"Banner with ID {id} not found for editing.");
                return NotFound();
            }
            return View(banner);
        }

        // POST: AdminBanners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,IsActive,CreatedAt")] Banner banner)
        {
            if (id != banner.Id)
            {
                _logger.LogError($"Banner ID mismatch: route ID {id}, model ID {banner.Id}.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    banner.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.Banners.Update(banner);
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation($"Banner '{banner.Title}' (ID: {banner.Id}) updated successfully.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"Concurrency error updating banner with ID {banner.Id}.");
                    // Check if the banner still exists before throwing
                    if (await _unitOfWork.Banners.GetByIdAsync(banner.Id) == null)
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
                    _logger.LogError(ex, $"Error updating banner with ID {banner.Id}.");
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning($"Banner (ID: {banner.Id}) edit failed due to invalid model state.");
            return View(banner);
        }

        // GET: AdminBanners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _unitOfWork.Banners.GetByIdAsync(id.Value);
            if (banner == null)
            {
                _logger.LogWarning($"Banner with ID {id} not found for deletion confirmation.");
                return NotFound();
            }
            return View(banner);
        }

        // POST: AdminBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _unitOfWork.Banners.GetByIdAsync(id);
            if (banner == null)
            {
                _logger.LogWarning($"Banner with ID {id} not found for confirmed deletion.");
                return NotFound();
            }

            await _unitOfWork.Banners.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Banner '{banner.Title}' (ID: {id}) deleted successfully.");
            return RedirectToAction(nameof(Index));
        }
    }
}
