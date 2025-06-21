using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userFromReq)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FullName = userFromReq.FullName,
                    Email = userFromReq.Email,
                    UserName = userFromReq.Email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                IdentityResult result = await _userManager.CreateAsync(user, userFromReq.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {user.UserName} created successfully.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    _logger.LogWarning($"User registration failed for {userFromReq.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            return View(userFromReq);
        }
        #endregion

        #region Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userFromReq, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userFromReq.Email, userFromReq.Password, userFromReq.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {userFromReq.Email} logged in successfully.");
                    var user = await _userManager.FindByEmailAsync(userFromReq.Email);
                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.FullName ?? user.Email),
                            new Claim(ClaimTypes.Email, user.Email)
                        };
                        await _signInManager.SignInWithClaimsAsync(user, userFromReq.RememberMe, claims);
                    }

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
                    _logger.LogWarning($"Failed login attempt for email: {userFromReq.Email}");
                }
            }
            return View(userFromReq);
        }
        #endregion

        #region Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login");
        }
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Welcome
        public IActionResult Welcome()
        {
            if (User.Identity.IsAuthenticated)
            {
                string name = User.Identity.Name;
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Content($"Welcome : {name} \t id={userId}");
            }
            return Content("Welcome Guest");
        }
        #endregion
    }
}
