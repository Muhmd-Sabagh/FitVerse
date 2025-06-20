using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels.Order
{
    [NotMapped]
    public class OrderItemViewModel
    {
   
        //public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = new DateTime(2025, 01, 01);        
        public decimal TotalPrice => UnitPrice * Quantity;

        // from product

        public string Prod_Name { get; set; }
        public string ImageUrl { get; set; }
        

    }
}
