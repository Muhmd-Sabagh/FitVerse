using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels.Order
{
    [NotMapped]
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Total price for this specific order item (calculated property)
        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice => UnitPrice * Quantity;

        // From the product for display
        [Display(Name = "Product Name")]
        public string Prod_Name { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }
    }
}
