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

        public async Task<PaymentType> GetPaymentTypeAsync(int id)
        {
            var entitity = await dbContext.PaymentTypes.Where(e => e.Id == id).FirstAsync();

            return entitity;
        }


    }
}
