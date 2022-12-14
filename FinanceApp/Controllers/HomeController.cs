using Microsoft.AspNetCore.Mvc;
using FinanceApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Home page
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("IndexLoggedIn");
            }
            return View();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel model)
        {
            return View(model);
        }
    }
}