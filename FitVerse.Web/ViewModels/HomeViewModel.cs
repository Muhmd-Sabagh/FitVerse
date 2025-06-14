using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Home;

namespace FitVerse.Web.ViewModels
{
    public class HomeViewModel
    {
       public List<Category> Categories { get; set; }
        //public List<BannarHomeViewModel> Banners { get; set; }

        public List<Banner> Banners { get; set; }
        public List<Product> Products { get; set; }

    }
}
