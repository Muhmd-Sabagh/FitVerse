using FitVerse.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.ViewModels
{
    public class DetailsViewModel
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
       public string categoryName { get; set; }
        public int selectedQuantity { get; set; } = 1;



        
        public List<Category> Categories { get; set; }
        public List<OrderItem> Items { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
