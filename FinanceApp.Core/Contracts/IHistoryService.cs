using FinanceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Contracts
{
    /// <summary>
    /// A service for past payments
    /// </summary>
    public interface IHistoryService
    {
        public string userId { get; set; }

        Task<IEnumerable<BudgetsHistory>> GetHistoryPaymentsByMonthAndYearAsync(int month, int year);

        Task<IEnumerable<CurrentPayment>> GetAllDeletedPayments();

        Task<IEnumerable<PaymentType>> GetAllDeletedPaymentTypes();

        Task UndoDeletedPayment(CurrentPayment entity);

        Task UndoDeletedPaymentType(PaymentType entity);
    }
}
