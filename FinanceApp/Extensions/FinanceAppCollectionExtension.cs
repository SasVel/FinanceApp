using FinanceApp.Core.Contracts;
using FinanceApp.Core.Services;
using FinanceApp.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// An extention for all services of the application
    /// </summary>
    public static class FinanceAppCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPaymentService, PaymentService>(); 
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IHistoryService, HistoryService>();

            return services;
        }

    }
}
