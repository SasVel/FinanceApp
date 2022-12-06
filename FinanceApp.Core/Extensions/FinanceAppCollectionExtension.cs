using FinanceApp.Core.Contracts;
using FinanceApp.Core.Services;
using FinanceApp.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FinanceAppCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPaymentService, PaymentService>(); 

            return services;
        }

    }
}
