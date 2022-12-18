using FinanceApp.Core.Common;
using FinanceApp.Core.Contracts;
using FinanceApp.Core.Helpers;
using FinanceApp.Infrastructure.Models;
using FinanceApp.Models;
using Ganss.XSS;
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
        private string currency;

        public DashboardController(IPaymentService _paymentService, IPaymentTypeService _paymentTypeService, UserManager<User> _userManager)
        {
            paymentService = _paymentService;
            paymentTypeService = _paymentTypeService;
            userManager = _userManager;

        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            budget = await paymentService.GetUsersFullMonthlyBudget();
            currency = await paymentService.GetUsersCurrency();
            if (budget == null || budget == 0 || currency == null)
            {
                return RedirectToAction("MonthlyBudgetForm");
            }
            else
            {
                var currentBudget = await paymentService.GetUsersCurrentBudget();
                var estimatedBudget = await paymentService.GetUsersEstimatedBudget();

                var paymentTypes = await paymentTypeService.GetAllActivePaymentTypes();
                var enumNames = Enum.GetNames(typeof(Currencies));
                var model = new DashboardViewModel()
                {
                    FullBudget = (decimal)budget,
                    CurrentBudget = (decimal)currentBudget,
                    EstimatedBudget = (decimal)estimatedBudget,
                    PaymentsInfo = paymentTypes.Select(pt => new PaymentTypeViewModel
                    {
                        Id = pt.Id,
                        Name = pt.Name,
                        CurrentPayments = pt.Payments.Select(p => new PaymentViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Cost = p.Cost,
                            IsSignular = p.IsSignular,
                            IsPaidFor = p.IsPaidFor,
                            PaymentTypeId = p.PaymentTypeId
                        })

                    }),
                    Currency = EnumHelper.GetEnumDescription((Currencies)Array.IndexOf(enumNames, currency))
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
            if (model.FullBudget == 0m || model.Currency == null)
            {
                ModelState.AddModelError("","Invalid budget or currency");
                return View(model);
            }
            await paymentService.SetUsersMonthlyBudget(model.FullBudget);
            await paymentService.SetUsersCurrency(model.Currency);

            return RedirectToAction("Index");
        }

    }
}
