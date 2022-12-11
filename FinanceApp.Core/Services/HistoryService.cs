using FinanceApp.Core.Contracts;
using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<CurrentPayment>> GetAllDeletedPayments()
        {
            var currentDeleted = await dbContext.CurrentPayments.Where(p => p.IsActive == false).ToListAsync();
            var pastDeleted = await dbContext.BudgetsHistory.Where(p => p.IsActive == false)
                .Select(p => new CurrentPayment()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Cost = p.Price,
                    EntryDate = p.EntryDate,
                    IsSignular = p.IsSingular,
                    IsPaidFor = p.IsPaidFor,
                    IsActive = p.IsActive,
                    UserId = p.UserId,
                    PaymentTypeId = p.PaymentTypeId,
                })
                .ToListAsync();

            currentDeleted.AddRange(pastDeleted);

            return currentDeleted;
        }

        public async Task<IEnumerable<PaymentType>> GetAllDeletedPaymentTypes()
        {
            var entities = await dbContext.PaymentTypes.Where(pt => pt.IsActive == false).ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<BudgetsHistory>> GetHistoryPaymentsByMonthAndYearAsync(int month, int year)
        {
            var entities = await Task.FromResult(dbContext.BudgetsHistory.Where(p => p.EntryDate.Month == month && p.EntryDate.Year == year).ToArray());

            return entities;
        }

        public async Task UndoDeletedPayment(CurrentPayment entity)
        {
            entity.IsActive = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task UndoDeletedPaymentType(PaymentType entity)
        {
            entity.IsActive = true;

            await dbContext.SaveChangesAsync();
        }
    }
}
