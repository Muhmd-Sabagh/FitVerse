using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitVerse.Web.Models;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Cart;
using Microsoft.AspNetCore.Http;

namespace FitVerse.Web.ViewModels.Checkout
{
    [NotMapped]
    public class Checkout_ViewModel
    {
        public Checkout_ViewModel()
        {
            CartItemsViewModels = new List<CartItem_ViewModel>();
        }
        public int UserId { get; set; }
        //public User User { get; set; }
        public DateTime OrderDate { get; set; }
        //public int TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int UnitPrice { get; set; }

        // from CartItems
        public List<CartItem_ViewModel> CartItemsViewModels { get; set; }
        public List<string> Prod_Names { get; set; }
        public List<int> EffectivePrice { get; set; }
        public List<string> ImageUrl { get; set; }


    }
}
