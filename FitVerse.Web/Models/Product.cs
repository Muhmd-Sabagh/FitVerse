using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Material { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? DiscountPercentage { get; set; }

        public bool IsNewArrival { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(0, 255)]
        public int StockQuantity { get; set; } = 0;

        public int CategoryId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public decimal EffectivePrice
        {
            get
            {
                if (IsOnSale)
                {
                    return Price * (1 - (DiscountPercentage.GetValueOrDefault() / 100m));
                }
                return Price;
            }
        }

        [NotMapped]
        public bool IsOnSale => DiscountPercentage.HasValue && DiscountPercentage > 0;

        // Navigation Property
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = default!;

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
