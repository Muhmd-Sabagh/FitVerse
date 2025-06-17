using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitVerse.Web.Models;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Http;

namespace FitVerse.Web.ViewModels.Checkout
{
    [NotMapped]
    public class Order_ViewModel
    {
        public Order_ViewModel()
        {
        }

        //public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; } = new DateTime(2025, 01, 01);

        //[Required]
        //[Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        //[Required]
        //[StringLength(50)]
        public string Status { get; set; } = "Pending";

        //[Required]
        //[StringLength(500)]
        public string ShippingAddress { get; set; }

        //[Requiro(100)]
        public string CustomerName { get; set; }

        //[Required]
        //[EmailAddress]
        //[StringLength(255)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = new DateTime(2025, 01, 01);

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; } = new DateTime(2025, 01, 01);

        // Navigation Properties
        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }
        //public virtual ICollection<OrderItem> OrderItems { get; set; }



    }
}
