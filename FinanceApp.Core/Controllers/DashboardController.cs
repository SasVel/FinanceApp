using FinanceApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Dashboard page
    /// </summary>
    public class DashboardController : Controller
    {
        private readonly IPaymentService paymentService;


        public DashboardController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
