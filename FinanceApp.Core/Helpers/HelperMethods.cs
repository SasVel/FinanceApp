using FinanceApp.Core.Contracts;
using FinanceApp.Models;

namespace FinanceApp.Helpers
{
    public static class HelperMethods
    {
        private static readonly IPaymentService paymentService;


        public static async Task<IEnumerable<PaymentTypeViewModel>> GetAllPaymentTypeViewModels()
        {
            var entities = await paymentService.GetAllPaymentTypes();
            var models = entities.Select(x => new PaymentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return models;
        }
    }
}
