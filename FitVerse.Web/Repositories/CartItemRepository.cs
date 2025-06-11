using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories
{
    public class CartItemRepository: GenericRepository<CartItem>
    {
        public CartItemRepository(FitVerseContext db) : base(db)
        {

        }
        //public List<Product> GetUserProducts()
        //{
        //    // get User Id
        //    //return Db.Products.Where(prod=>prod.Id == UserId);
        //}
    }
}
