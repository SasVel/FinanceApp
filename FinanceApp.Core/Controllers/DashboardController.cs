using FinanceApp.Core.Contracts;
using FinanceApp.Infrastructure.Models;
using FinanceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Dashboard page
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly UserManager<User> userManager;

        private decimal? budget;

        public DashboardController(IPaymentService _paymentService, UserManager<User> _userManager)
        {
            paymentService = _paymentService;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            budget = await paymentService.GetUsersFullMonthlyBudget(currentUser.Id);
            if (budget == null || budget == 0)
            {
                return RedirectToAction("MonthlyBudgetForm");
            }
            else
            {
                var currentBudget = await paymentService.GetUsersCurrentBudget(currentUser.Id);
                var estimatedBudget = await paymentService.GetUsersEstimatedBudget(currentUser.Id);
                var model = new DashboardViewModel()
                {
                    FullBudget = (decimal)budget,
                    CurrentBudget = (decimal)currentBudget,
                    EstimatedBudget = (decimal)estimatedBudget
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult MonthlyBudgetForm()
        {
            var model = new DashboardViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MonthlyBudgetForm(DashboardViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);
            await paymentService.SetUsersMonthlyBudget(currentUser.Id, model.FullBudget);

            return RedirectToAction("Index");
        }

    }
}
