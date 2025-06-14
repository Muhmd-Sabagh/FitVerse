using FitVerse.Web.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork;
        public ProductController(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
