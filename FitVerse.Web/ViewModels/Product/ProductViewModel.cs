namespace FitVerse.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Material { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public bool IsNewArrival { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public string ImageUrl { get; set; }

        public int StockQuantity { get; set; } = 0;

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // public CategoryViewModel Category { get; set; }
    }
}
