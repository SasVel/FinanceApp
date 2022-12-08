using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using FinanceApp.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Core.Services
{
    /// <summary>
    /// A service for all actions connected to payments
    /// </summary>
    public class PaymentService : IPaymentService
    {
        public readonly ApplicationDbContext dbContext;

        public PaymentService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddCurrentPaymentAsync(CurrentPayment entry)
        {
            await dbContext.CurrentPayments.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddPaymentTypeAsync(PaymentType entry)
        {
            await dbContext.PaymentTypes.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurrentPayment>> GetAllCurrentPayments()
        {
            var entities = await dbContext.CurrentPayments.ToArrayAsync();

            return entities;
        }

        public async Task<IEnumerable<CurrentPayment>> GetAllPaymentsByTypeIdAsync(int PaymentTypeId)
        {
            var entities = await dbContext.CurrentPayments.Where(p => p.PaymentTypeId == PaymentTypeId).ToArrayAsync();

            return entities;
        }

        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypes()
        {
            var entities = await dbContext.PaymentTypes.ToArrayAsync();

            return entities;
        }
        /// <summary>
        /// Gets the payments that are paid or not. True for paid, false for not paid for.
        /// </summary>
        /// <param name="isSingular"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsIsPaidFor(bool isPaid)
        {
            if (isPaid)
            {
                return await dbContext.CurrentPayments.Where(p => p.IsPaidFor == true).ToArrayAsync();
            }
            else
            {
                return await dbContext.CurrentPayments.Where(p => p.IsPaidFor == false).ToArrayAsync();
            }
        }

        /// <summary>
        /// Gets the singular or recurring current payments. True for singular, false for recurring
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsSingular(bool isSingular)
        {
            if (isSingular)
            {
                return await dbContext.CurrentPayments.Where(p => p.IsSignular == true).ToArrayAsync();
            }
            else
            {
                return await dbContext.CurrentPayments.Where(p => p.IsSignular == false).ToArrayAsync();
            }
        }

        public async Task<PaymentType> GetPaymentTypeAsync(int id)
        {
            var entitity = await dbContext.PaymentTypes.Where(e => e.Id == id).FirstAsync();

            return entitity;
        }

        //public async Task<decimal> GetUsersMonthlyBudget(string id)
        //{
        //    return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        //}

        public Task SetUsersMonthlyBudget(decimal newBudget)
        {
            throw new NotImplementedException();
        }
    }
}
