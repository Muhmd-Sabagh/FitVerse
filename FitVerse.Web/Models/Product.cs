using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Material { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
        public decimal? DiscountPercentage { get; set; }

        public bool IsNewArrival { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        public int StockQuantity { get; set; } = 0;

        [Required]
        public int CategoryId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = new DateTime(2024, 01, 01);

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; } = new DateTime(2024, 01, 01);

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // Calculated Properties
        public decimal EffectivePrice
        {
            get
            {
                if (IsOnSale)
                {
                    // Calculate the discounted price
                    return Price * (1 - (DiscountPercentage.GetValueOrDefault() / 100m));
                }
                return Price; // Return original price if no discount
            }
        }

        public bool IsOnSale => DiscountPercentage.HasValue && DiscountPercentage > 0;
    }
}
