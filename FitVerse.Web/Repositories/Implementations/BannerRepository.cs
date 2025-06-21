using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        public BannerRepository(FitVerseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Banner>> GetActiveBannersAsync()
        {
            return await _dbSet.Where(b => b.IsActive).ToListAsync();
        }
    }
}
