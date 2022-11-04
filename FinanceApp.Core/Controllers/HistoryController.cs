using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
