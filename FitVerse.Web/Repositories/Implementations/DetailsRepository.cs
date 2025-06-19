using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Repositories.Implementations
{
    public class DetailsRepository : IProduct
    {
        private readonly FitVerseContext context;
        
        public DetailsRepository(FitVerseContext context)
        {
            this.context = context;
        }

        public Product GetById(int id)
        {
            return context.Products.Include(p => p.Category)
                .Include(p => p.CartItems)
                .Include(p => p.OrderItems)
                .Include(p=>p.Category)
                .FirstOrDefault(p => p.Id == id);
                
        }
        public void Add(Product obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product obj)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

       public string GetParentCategoryByChildId(int prodId)
        {
            Product product = context.Products.Where(p=>p.Id == prodId).FirstOrDefault(); 
            Category category = context.Categories.Where(c => c.Id == product.CategoryId).FirstOrDefault();
            
            return category.ParentCategory.Name;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
