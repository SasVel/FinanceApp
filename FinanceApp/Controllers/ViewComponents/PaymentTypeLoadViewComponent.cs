using FinanceApp.Core.Contracts;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers.ViewComponents
{
    public class PaymentTypeLoadViewComponent : ViewComponent
    {
        private readonly IPaymentTypeService paymentTypeService;

        public PaymentTypeLoadViewComponent(IPaymentTypeService paymentTypeService)
        {
            this.paymentTypeService = paymentTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entities = await paymentTypeService.GetAllPaymentTypes();
            if (entities != null)
            {
                var models = entities.Select(e => new PaymentTypeViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                });
                return View(models);
            }
            else
            {
                var models = new List<PaymentTypeViewModel>();
                return View(models);
            }
        }

    }
}
