using FitVerse.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Material { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }

        public decimal EffectivePrice { get; set; }
        public bool IsNewArrival { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string? ParentCategory { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int SelectedQuantity { get; set; } = 1;

        public bool IsOnSale { get; set; }

        public List<Models.Category>? Categories { get; set; }
        public List<OrderItem>? Items { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
