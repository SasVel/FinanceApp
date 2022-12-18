using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.Infrastructure.Models;
using FinanceApp.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Ganss.XSS;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// A controller for the User pages.
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private IHtmlSanitizer sanitizer;

        public UserManager<User> UserManager => userManager;

        public UserController(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;

            sanitizer = new HtmlSanitizer();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {

                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = new User()
            {
                Email = sanitizer.Sanitize(model.Email),
                UserName = sanitizer?.Sanitize(model.UserName),
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                return RedirectToAction("Index", "Dashboard");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.UserName = sanitizer.Sanitize(model.UserName);
            model.Password = sanitizer.Sanitize(model.Password);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "The account is locked out");
                    return View();
                }
            }

            


            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
    }
}
