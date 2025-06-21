using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Product;

namespace FitVerse.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<BannarHomeViewModel> Banners { get; set; } = new List<BannarHomeViewModel>();
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
