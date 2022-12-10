using FinanceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Contracts
{
    public interface IHistoryService
    {
        Task<IEnumerable<BudgetsHistory>> GetHistoryPaymentsByMonthAndYearAsync(int month, int year);


    }
}
