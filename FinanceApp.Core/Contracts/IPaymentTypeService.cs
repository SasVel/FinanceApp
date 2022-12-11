using FinanceApp.Infrastructure.Models;

namespace FinanceApp.Core.Contracts
{
    /// <summary>
    /// An interface for the PaymentTypeService class
    /// </summary>
    public interface IPaymentTypeService
    {
        Task AddPaymentTypeAsync(PaymentType entry);

        Task<PaymentType?> GetPaymentTypeAsync(int id);

        Task<PaymentType> GetInactivePaymentTypeAsync(int id);

        Task<IEnumerable<PaymentType?>> GetAllActivePaymentTypes();

        Task<IEnumerable<PaymentType?>> GetAllInactivePaymentTypes();

        Task DeletePaymentType(int Id);

        Task SaveChangesToPaymentTypeAsync();

    }
}
