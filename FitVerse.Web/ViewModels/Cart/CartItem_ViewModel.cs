using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FitVerse.Web.Models;

namespace FitVerse.Web.ViewModels.Cart
{
    [NotMapped]
    public class CartItem_ViewModel
    {
        // from Cart Model
        public int Cart_Id { get; set; }
        //public int UserId { get; set; }
        public int Quantity { get; set; }
        //public DateTime CreatedAt { get; set; } 
        //public DateTime UpdatedAt { get; set; }
        public decimal TotalPrice;


        // from Product Model
        public int Prod_Id { get; set; }
        [Display(Name = "Product")]
        public string Prod_Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        [Display(Name = "")]
        public string ImageUrl { get; set; }
        [Display(Name = "Cost")]
        public decimal EffectivePrice { get; set; }
    }
}
