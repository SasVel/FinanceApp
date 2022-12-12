using FinanceApp.Infrastructure.Models;

namespace FinanceApp.Core.Contracts
{
    /// <summary>
    /// An interface for the PaymentService class
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Returns all the current payments.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CurrentPayment>> GetAllCurrentPayments();

        /// <summary>
        /// Returns a payment by id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CurrentPayment> GetPaymentAsync(int id);

        /// <summary>
        /// Gets the singular or recurring current payments. True for singular, false for recurring
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsSingular(bool isSingular);

        /// <summary>
        /// Gets the payments that are paid or not. True for paid, false for not paid for.
        /// </summary>
        /// <param name="isPaid"></param>
        /// <returns></returns>
        Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsIsPaidFor(bool isPaid);

        Task<decimal?> GetUsersFullMonthlyBudget(string id);

        /// <summary>
        /// Returns the current budget of the user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<decimal?> GetUsersCurrentBudget(string id);

        /// <summary>
        /// Returns the estimated budget of the user by id. 
        /// Money that are going to be left at the end of the month.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<decimal?> GetUsersEstimatedBudget(string id);

        /// <summary>
        /// Sets the users monthly budget.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBudget"></param>
        /// <returns></returns>
        Task SetUsersMonthlyBudget(string id, decimal newBudget);

        /// <summary>
        /// Adds a current payment.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        Task AddCurrentPaymentAsync(CurrentPayment entry);

        /// <summary>
        /// Returns all payments that weren't deleted.
        /// </summary>
        /// <param name="PaymentTypeId"></param>
        /// <returns></returns>
        Task<IEnumerable<CurrentPayment>> GetAllActivePaymentsByTypeIdAsync(int PaymentTypeId);

        Task PayForPayment(int id);

        /// <summary>
        /// Sets the payment in a not paid state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task UndoPayment(int id);

        /// <summary>
        /// Sets the payment as inactive.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePayment(int id);
    }
}
