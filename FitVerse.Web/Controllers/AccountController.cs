using FitVerse.Web.Models;
using FitVerse.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        //Ctor
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Register 
        public IActionResult Register()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] ///deny any req from out our domain or out our website
        public async Task<IActionResult> Register(RegisterViewModel userFromReq)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                { 
                    FullName= userFromReq.FullName,
                    Email= userFromReq.Email,
                    UserName=userFromReq.Email
                };

                //Save using userManager
                IdentityResult result = await userManager.CreateAsync(user,userFromReq.Password);
                if (result.Succeeded)
                {
                    //Create Cookie
                     await signInManager.SignInAsync(user,false); //take data from user and save it in cookie -- ispresistent :false = session
                    //redirect any action need to authorized
                    return RedirectToAction("login", "Account");    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("password", error.Description);
                }

            }
            return View(userFromReq);
        }


        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userFromReq)
        {
            if (ModelState.IsValid)
            {
                // check -- create cookie
               ApplicationUser userFromDb= await userManager.FindByEmailAsync(userFromReq.Email);
                if (userFromDb != null)
                {
                    bool found =await userManager.CheckPasswordAsync(userFromDb, userFromReq.Password);
                    if (found)
                    {
                        // Create the FullName claim
                        var claims = new List<Claim>
                            {
                                new Claim("FullName", userFromDb.FullName ?? userFromDb.UserName)
                            };
                        // Sign in with claims
                        await signInManager.SignInWithClaimsAsync(userFromDb, userFromReq.RememberMe, claims);
                        //create cookie 
                       // await signInManager.SignInAsync(userFromDb,userFromReq.RememberMe); //create cookie and data can get from 'User.Identity'
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            
            }
            return View(userFromReq);
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }
        // this controller if U want to great Welcome (user name)
        #region Welcome

        public IActionResult Welcome()
        {
            if (User.Identity.IsAuthenticated == true)
            { 
                string name = User.Identity.Name;
                // this step for U 'Mark' ==> get id from claims
                Claim idClaim=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier);
                return Content($"Welcome : {name} \t id={idClaim}");
            }
            //Guest
            return Content("Welcome Guest");
        }

        #endregion

    }
}
