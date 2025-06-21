using FitVerse.Web.ViewModels.Cart;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Web.ViewModels.Checkout
{
    [NotMapped]
    public class Checkout_ViewModel
    {
        public Checkout_ViewModel()
        {
            CartItemsViewModels = new List<CartItem_ViewModel>();
            Prod_Names = new List<string>();
            EffectivePrice = new List<decimal>();
            ImageUrl = new List<string>();
        }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Shipping Address is required.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Customer Phone is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string CustomerPhone { get; set; }
        public decimal UnitPrice { get; set; }

        // from CartItems - list of cart items to be checked out
        public List<CartItem_ViewModel> CartItemsViewModels { get; set; }
        public List<string> Prod_Names { get; set; }
        public List<decimal> EffectivePrice { get; set; }
        public List<string> ImageUrl { get; set; }
    }
}
