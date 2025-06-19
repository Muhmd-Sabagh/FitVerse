using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Home;
using FitVerse.Web.ViewModels.Product;

namespace FitVerse.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<BannarHomeViewModel> Banners { get; set; }

        //public List<Banner> Banners { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}