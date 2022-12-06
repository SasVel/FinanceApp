using FinanceApp.Core.Contracts;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers.ViewComponents
{
    public class PaymentTypeLoadViewComponent : ViewComponent
    {
        private readonly IPaymentService paymentService;

        public PaymentTypeLoadViewComponent(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entities = await paymentService.GetAllPaymentTypes();
            var models = entities.Select(e => new PaymentTypeViewModel()
            {
                Id = e.Id,
                Name = e.Name,
            });

            return View(models);
        }

    }
}
