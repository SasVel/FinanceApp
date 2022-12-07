using FinanceApp.Core.Contracts;
using FinanceApp.Models;

namespace FinanceApp.Helpers
{
    public static class HelperMethods
    {
        private static readonly IPaymentTypeService paymentTypeService;

        public static async Task<IEnumerable<PaymentTypeViewModel>> GetAllPaymentTypeViewModels()
        {
            var entities = await paymentTypeService.GetAllPaymentTypes();
            var models = entities.Select(x => new PaymentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return models;
        }
    }
}
