using FitVerse.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.ViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public int EffectivePrice { get; set; } 
        public bool IsNewArrival { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }

        public string CategoryName { get; set; }
        public string ParentCategory {  get; set; }

        public int SelectedQuantity { get; set; } = 1;


        public bool IsOnSale { get; set; }


        public List<Category> Categories { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
