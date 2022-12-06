using FinanceApp.Core.Contracts;
using FinanceApp.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FinanceAppCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }

    }
}
