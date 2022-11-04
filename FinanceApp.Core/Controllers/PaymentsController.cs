using FinanceApp.Data.Models;
using FinanceApp.Models;
using FinanceApp.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
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
