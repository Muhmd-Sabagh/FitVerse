using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FitVerse.Web.Models;

namespace FitVerse.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Material { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }

        public bool IsNewArrival { get; set; }
        public bool IsActive { get; set; }

        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }

        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }

        public decimal EffectivePrice { get; set; }
        public bool IsOnSale { get; set; }
    }
}
