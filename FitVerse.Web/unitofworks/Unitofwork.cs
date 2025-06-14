using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Implementations;

namespace FitVerse.Web.unitofworks
{
    public class Unitofwork 
    {
        GenericRepo<Banner> banner;
        GenericRepo<CartItem> cartItem;
        //GenericRepo<Category> category;//عشان هو وارثها من ال categoryrepo
        GenericRepo<Order> order;
        GenericRepo<OrderItem> orderItem;
        //GenericRepo<Product> product;
        GenericRepo<User> user;
        CategoryRepo categoryRepo;
        ProductRepo productRepo;

        public FitVerseContext Fit { get; }

        public Unitofwork(FitVerseContext fit) {
            Fit = fit;
        }
        public GenericRepo<Banner> Banner {
            get {
                if (banner == null)
                    banner = new GenericRepo<Banner>(Fit);
                return banner;    
                        } }
        public CategoryRepo CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                    categoryRepo = new CategoryRepo(Fit);
                return categoryRepo;
            }
        }
        public GenericRepo<CartItem> CartItem
        {
            get
            {
                if (cartItem == null)
                    cartItem = new GenericRepo<CartItem>(Fit);
                return cartItem;
            }
        }
        public ProductRepo ProductRepo
        {
            get
            {
                if (productRepo == null)
                    productRepo = new ProductRepo(Fit);
                return productRepo;
            }
        }
        public GenericRepo<Order> Order
        {
            get
            {
                if (order == null)
                    order = new GenericRepo<Order>(Fit);
                return order;
            }
        }
        public GenericRepo<OrderItem> OrderItem
        {
            get
            {
                if (orderItem == null)
                    orderItem = new GenericRepo<OrderItem>(Fit);
                return orderItem;
            }
        }
      
        public GenericRepo<User> User
        {
            get
            {
                if (user == null)
                    user = new GenericRepo<User>(Fit);
                return user;
            }

        }

    }
}
