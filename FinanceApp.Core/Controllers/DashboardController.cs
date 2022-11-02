using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
