using FitVerse.Web.Models;
using FitVerse.Web.GenericRepos;

namespace FitVerse.Web.unitofworks
{
    public class Unitofwork 
    {
        GenericRepo<Banner> banner;
        GenericRepo<CartItem> cartItem;
        GenericRepo<Category> category;
        GenericRepo<Order> order;
        GenericRepo<OrderItem> orderItem;
        GenericRepo<Product> product;
        GenericRepo<User> user;



        public Unitofwork(FitVerseContext fit) {
            Fit = fit;
        }
        public GenericRepo<Banner> Banner {
            get {
                if (banner == null)
                    banner = new GenericRepo<Banner>();
                return banner;    
                        } }
        public GenericRepo<CartItem> CartItem
        {
            get
            {
                if (cartItem == null)
                    cartItem = new GenericRepo<CartItem>();
                return cartItem;
            }
        }
        public GenericRepo<Category> Category
        {
            get
            {
                if (category == null)
                    category = new GenericRepo<Category>();
                return category;
            }
        }
        public GenericRepo<Order> Order
        {
            get
            {
                if (order == null)
                    order = new GenericRepo<Order>();
                return order;
            }
        }
        public GenericRepo<OrderItem> OrderItem
        {
            get
            {
                if (orderItem == null)
                    orderItem = new GenericRepo<OrderItem>();
                return orderItem;
            }
        }
        public GenericRepo<Product> Product
        {
            get
            {
                if (product == null)
                    product = new GenericRepo<Product>();
                return product;
            }
        }
        public GenericRepo<User> User
        {
            get
            {
                if (user == null)
                    user = new GenericRepo<User>();
                return user;
            }
        }
        public FitVerseContext Fit { get; }
    }
}
