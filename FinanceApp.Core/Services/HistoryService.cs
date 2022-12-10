using FinanceApp.Core.Contracts;
using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Services
{
    /// <summary>
    /// A service for past payments
    /// </summary>
    public class HistoryService : IHistoryService
    {
        public readonly ApplicationDbContext dbContext;

        public HistoryService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<IEnumerable<BudgetsHistory>> GetHistoryPaymentsByMonthAndYearAsync(int month, int year)
        {
            var entities = await Task.FromResult(dbContext.BudgetsHistory.Where(p => p.EntryDate.Month == month && p.EntryDate.Year == year).ToArray());

            return entities;
        }
    }
}
