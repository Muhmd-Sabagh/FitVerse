using System.Net.Http.Headers;
using E_commerce_Khotwa.Models;

namespace E_commerce_Khotwa.Repository
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(MarkITIContext db) : base(db)
        {

        }
        public List<Product> GetUserProducts()
        {
            // get User Id
            //return Db.Products.Where(prod=>prod.Id == UserId);
        }
    }
}
