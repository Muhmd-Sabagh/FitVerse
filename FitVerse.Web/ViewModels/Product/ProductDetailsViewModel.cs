using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(255, ErrorMessage = "Product name cannot exceed 255 characters.")]
        public string Name { get; set; }

        public string? Material { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 100000.00, ErrorMessage = "Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal? DiscountPercentage { get; set; }

        public bool IsNewArrival { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid Image URL format.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        public string ParentCategoryName { get; set; }

        public string CategoryName { get; set; }

        public decimal EffectivePrice { get; set; }

        public bool IsOnSale { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int SelectedQuantity { get; set; } = 1;
    }
}
