using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinanceApp.Models;
using FinanceApp.Core.Contracts;
using System.Globalization;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the history page
    /// </summary>
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly IHistoryService historyService;
        private readonly IPaymentTypeService paymentTypeService;

        public HistoryController(IHistoryService _historyService, IPaymentTypeService _paymentTypeService)
        {
            historyService = _historyService;
            paymentTypeService = _paymentTypeService;
        }

        public IActionResult Index(DashboardViewModel dashboardModel)
        {
            if (dashboardModel.PaymentsInfo == null)
            {
                var model = new HistoryViewModel();
                return View(model);
            }
            else
            {
                var model = new HistoryViewModel()
                {
                    Result = dashboardModel
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(HistoryViewModel model)
        {
            var paymentEntities = await historyService.GetHistoryPaymentsByMonthAndYearAsync(model.SearchMonth, model.SearchYear);
            var paymentModels = paymentEntities.Select(e => new CurrentPaymentViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Cost = e.Price,
                IsSignular = e.IsSingular,
                IsPaidFor = e.IsPaidFor,
                PaymentTypeId = e.PaymentTypeId
            });
            var paymentTypeEntities = await Task.FromResult(paymentTypeService
                .GetAllPaymentTypes().Result
                .Where(pt => paymentModels
                .Select(pm => pm.PaymentTypeId)
                .Contains(pt.Id))
                .ToArray());
            var dashboardModel = new DashboardViewModel()
            {
                PaymentsInfo = paymentTypeEntities.Select(pte => new PaymentTypeViewModel()
                {
                    Id = pte.Id,
                    Name = pte.Name,
                    CurrentPayments = paymentModels.Where(pm => pm.PaymentTypeId == pte.Id)
                })
            };
            return RedirectToAction("Index", new { dashboardModel = dashboardModel });
        }
    }
}
