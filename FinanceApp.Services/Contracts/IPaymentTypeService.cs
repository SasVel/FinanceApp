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

        Task<IEnumerable<PaymentType?>> GetAllPaymentTypes();

        Task DeletePaymentType(int Id);

    }
}
