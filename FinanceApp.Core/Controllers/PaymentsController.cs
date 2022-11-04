using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
