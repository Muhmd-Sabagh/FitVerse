using FitVerse.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleFromReq)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = roleFromReq.RoleName,
                };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
