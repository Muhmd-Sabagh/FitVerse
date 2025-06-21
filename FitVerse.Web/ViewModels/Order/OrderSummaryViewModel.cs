using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels.Order
{
    public class OrderSummaryViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Email")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "Customer Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        // List of items within this order
        public List<OrderItemViewModel> OrderItems { get; set; }

        public OrderSummaryViewModel()
        {
            OrderItems = new List<OrderItemViewModel>();
        }
    }
}
