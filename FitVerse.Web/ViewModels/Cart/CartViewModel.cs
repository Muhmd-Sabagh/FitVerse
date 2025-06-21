namespace FitVerse.Web.ViewModels.Cart
{
    public class CartViewModel
    {
        // List of individual cart items, using the CartItem_ViewModel for presentation
        public List<CartItem_ViewModel> CartItems { get; set; }

        public decimal TotalCost { get; set; }

        public CartViewModel()
        {
            CartItems = new List<CartItem_ViewModel>();
        }
    }
}
