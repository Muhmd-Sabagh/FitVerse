using FitVerse.Web.Models;

namespace FitVerse.Web.Interfaces
{
    public interface IBannerRepository
    {
        Task<Banner> GetBannerByIdAsync(int id);
        Task<IEnumerable<Banner>> GetAllBannersAsync();
        Task AddBannerAsync(Banner banner);
        Task UpdateBannerAsync(Banner banner);
        Task DeleteBannerAsync(int id);
    }
}
