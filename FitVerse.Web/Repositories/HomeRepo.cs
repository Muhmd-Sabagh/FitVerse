using FitVerse.Web.GenericRepos;
using FitVerse.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace FitVerse.Web.Repositories
{
    public class HomeRepo<T> : GenericRepo<T> where T: class
    {
       
    }
}
