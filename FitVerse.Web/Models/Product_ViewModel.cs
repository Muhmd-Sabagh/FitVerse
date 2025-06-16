using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.Models
{
    public class Product_ViewModel
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
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public decimal EffectivePrice { get; set; }
        
        public bool IsOnSale { get; set; }
    }
}
