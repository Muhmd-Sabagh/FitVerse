using FitVerse.Web.Models;

namespace FitVerse.Web.Repositories.Implementations
{
    public class ProductRepo: GenericRepo<Product>
    {
        public ProductRepo(FitVerseContext fit): base(fit)
        { }
        public Product getbyname(string name)
        {
            return Fit.Products.First(p => p.Name == name);
        }
    }
}
