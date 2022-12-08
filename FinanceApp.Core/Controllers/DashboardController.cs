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
        private readonly IPaymentTypeService paymentTypeService;
        private readonly UserManager<User> userManager;

        private decimal? budget;

        public DashboardController(IPaymentService _paymentService, IPaymentTypeService _paymentTypeService, UserManager<User> _userManager)
        {
            paymentService = _paymentService;
            paymentTypeService = _paymentTypeService;
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

                var paymentTypes = await paymentTypeService.GetAllPaymentTypes();
                var model = new DashboardViewModel()
                {
                    FullBudget = (decimal)budget,
                    CurrentBudget = (decimal)currentBudget,
                    EstimatedBudget = (decimal)estimatedBudget,
                    PaymentsInfo = paymentTypes.Select(pt => new PaymentTypeViewModel
                    {
                        Id = pt.Id,
                        Name = pt.Name,
                        CurrentPayments = pt.Payments.Select(p => new CurrentPaymentViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Cost = p.Cost,
                            IsSignular = p.IsSignular,
                            IsPaidFor = p.IsPaidFor,
                            PaymentTypeId = p.PaymentTypeId
                        })

                    })
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
