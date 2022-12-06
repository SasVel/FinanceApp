using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Dashboard page
    /// </summary>
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
