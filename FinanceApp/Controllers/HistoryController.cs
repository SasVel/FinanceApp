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
        private readonly IPaymentService paymentService;
        private readonly IPaymentTypeService paymentTypeService;

        public HistoryController(IHistoryService _historyService, IPaymentService _paymentService, IPaymentTypeService _paymentTypeService)
        {
            historyService = _historyService;
            paymentService = _paymentService;
            paymentTypeService = _paymentTypeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
                var model = new HistoryViewModel();
                return View(model);
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
                .GetAllActivePaymentTypes().Result
                .Where(pt => paymentModels
                .Select(pm => pm.PaymentTypeId)
                .Contains(pt.Id))
                .ToArray());
            var historyModel = new HistoryViewModel()
            {
                Result = new DashboardViewModel()
                {
                    PaymentsInfo = paymentTypeEntities.Select(pte => new PaymentTypeViewModel()
                    {
                        Id = pte.Id,
                        Name = pte.Name,
                        CurrentPayments = paymentModels.Where(pm => pm.PaymentTypeId == pte.Id)
                    })
                }
            };
            return View(historyModel);
        }

        [HttpGet]
        public async Task<IActionResult> Undo()
        {
            var paymentEntities = await historyService.GetAllDeletedPayments();
            var paymentModels = paymentEntities.Select(e => new CurrentPaymentViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Cost = e.Cost,
                IsSignular = e.IsSignular,
                IsPaidFor = e.IsPaidFor,
                PaymentTypeId = e.PaymentTypeId
            });
            var paymentTypeEntities = await historyService.GetAllDeletedPaymentTypes();
            var paymentTypeModels = paymentTypeEntities.Select(e => new PaymentTypeViewModel()
            {
                Id= e.Id,
                Name= e.Name
            });
            var UndoModel = new UndoViewModel()
            {
                DeletedPayments = paymentModels,
                DeletedPaymentTypes = paymentTypeModels
            };
            
            return View(UndoModel);
        }

        [HttpGet]
        public async Task<IActionResult> UndoPaymentType(int id)
        {
            var entity = await paymentTypeService.GetInactivePaymentTypeAsync(id);
            await historyService.UndoDeletedPaymentType(entity);

            return RedirectToAction("Undo");
        }

        [HttpGet]
        public async Task<IActionResult> UndoPayment(int id)
        {
            var entity = await paymentService.GetPaymentAsync(id);
            await historyService.UndoDeletedPayment(entity);

            return RedirectToAction("Undo");
        }

    }
}
