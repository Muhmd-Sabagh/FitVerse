using FitVerse.Web.Interfaces;
using FitVerse.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        private readonly FitVerseContext _context;

        public BannerRepository(FitVerseContext context)
        {
            _context = context;
        }

        public async Task<Banner> GetBannerByIdAsync(int id)
        {
            return await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Banner>> GetAllBannersAsync()
        {
            return await _context.Banners.OrderBy(b => b.DisplayOrder).ToListAsync();
        }

        public async Task AddBannerAsync(Banner banner)
        {
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBannerAsync(Banner banner)
        {
            _context.Banners.Update(banner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBannerAsync(int id)
        {
            var banner = await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
