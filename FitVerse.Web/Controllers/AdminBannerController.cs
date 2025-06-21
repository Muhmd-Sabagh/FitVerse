using FitVerse.Web.Repositories.Interfaces;
using FitVerse.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBannersController : Controller
    {
        private readonly IBannerRepository _bannerRepository; // Assuming you create this repository

        public AdminBannersController(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        // GET: AdminBanners
        public async Task<IActionResult> Index()
        {
            return View(await _bannerRepository.GetAllBannersAsync());
        }

        // GET: AdminBanners/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var banner = await _bannerRepository.GetBannerByIdAsync(id);
            if (banner == null)
            {
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
        public async Task<IActionResult> Create([Bind("Title,Description,ImageUrl,LinkUrl,DisplayOrder")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                await _bannerRepository.AddBannerAsync(banner);
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: AdminBanners/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _bannerRepository.GetBannerByIdAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: AdminBanners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,LinkUrl,DisplayOrder,CreatedAt,UpdatedAt")] Banner banner)
        {
            if (id != banner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bannerRepository.UpdateBannerAsync(banner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _bannerRepository.GetBannerByIdAsync(banner.Id) == null)
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
            return View(banner);
        }

        // GET: AdminBanners/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _bannerRepository.GetBannerByIdAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: AdminBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bannerRepository.DeleteBannerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
