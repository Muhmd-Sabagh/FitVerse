using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.Models
{
    [NotMapped]
    public class CartItem_ViewModel
    {
        // from Cart Model
        public int Cart_Id { get; set; }
        //public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //public DateTime CreatedAt { get; set; } 
        //public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice ;


        // from Product Model
        public int Prod_Id { get; set; }
        public string Prod_Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string ImageUrl { get; set; }
        //public int StockQuantity { get; set; } = 0;
        public decimal EffectivePrice { get; set; }

    }
}
