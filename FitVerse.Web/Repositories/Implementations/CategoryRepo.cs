using FitVerse.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace FitVerse.Web.Repositories.Implementations
{
    public class CategoryRepo: GenericRepo<Category>
    {
        public CategoryRepo(FitVerseContext fit):base(fit)
        { 
        }
        public Category getbyname(string name)
        {
           return Fit.Categories.FirstOrDefault(n=>n.Name==name);

        }

    }
}
