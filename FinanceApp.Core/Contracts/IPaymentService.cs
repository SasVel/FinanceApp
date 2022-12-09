using FinanceApp.Infrastructure.Models;

namespace FinanceApp.Core.Contracts
{
    /// <summary>
    /// An interface for the PaymentService class
    /// </summary>
    public interface IPaymentService
    {
        Task<IEnumerable<CurrentPayment>> GetAllCurrentPayments();

        Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsSingular(bool isSingular);

        Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsIsPaidFor(bool isPaid);

        Task<decimal?> GetUsersFullMonthlyBudget(string id);

        Task<decimal?> GetUsersCurrentBudget(string id);

        Task<decimal?> GetUsersEstimatedBudget(string id);

        Task SetUsersMonthlyBudget(string id, decimal newBudget);

        Task AddCurrentPaymentAsync(CurrentPayment entry);

        Task<IEnumerable<CurrentPayment>> GetAllPaymentsByTypeIdAsync(int PaymentTypeId);

        Task PayForPayment(int id);

        Task UndoPayment(int id);
    }
}
