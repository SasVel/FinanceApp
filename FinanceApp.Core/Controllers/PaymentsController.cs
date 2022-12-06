using FinanceApp.Infrastructure.Models;
using FinanceApp.Models;
using FinanceApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Payments pages
    /// </summary>
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await paymentService.GetAllPaymentTypes();
            var models = entities.Select(x => new PaymentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });

            return View(models);
        }

        public async Task<IActionResult> SelectPaymentType(int id)
        {
            var entity = await paymentService.GetPaymentTypeAsync(id);
            var payments = await paymentService.GetAllPaymentsByTypeIdAsync(id);
            var model = new PaymentTypeViewModel()
            {
                Id = entity.Id,
                Name= entity.Name,
                CurrentPayments = payments
                    .Select(p => new CurrentPaymentViewModel() 
                    { 
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Cost = p.Cost,
                        IsSignular = p.IsSignular,
                        IsPayedFor = p.IsPayedFor
                    })
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult AddCurrentPayment()
        {
            var model = new CurrentPaymentViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCurrentPayment(CurrentPaymentViewModel model)
        {
            var entry = new CurrentPayment()
            {
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                IsSignular = model.IsSignular
            };

            await paymentService.AddCurrentPaymentAsync(entry);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddPaymentType()
        {
            var model = new PaymentTypeViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPaymentType(PaymentTypeViewModel model)
        {
            var entry = new PaymentType()
            {
                Name = model.Name
            };

            await paymentService.AddPaymentTypeAsync(entry);

            return RedirectToAction("Index");
        }

    }
}
