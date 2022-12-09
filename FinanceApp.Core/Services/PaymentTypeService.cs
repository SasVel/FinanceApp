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
    public class PaymentTypeService : IPaymentTypeService
    {
        public readonly ApplicationDbContext dbContext;

        public PaymentTypeService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task AddPaymentTypeAsync(PaymentType entry)
        {
            await dbContext.PaymentTypes.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentType?>> GetAllPaymentTypes()
        {
            var entities = await dbContext.PaymentTypes.Include(p => p.Payments).Where(p => p.IsActive == true).ToArrayAsync();

            return entities;
        }

        public async Task<PaymentType?> GetPaymentTypeAsync(int id)
        {
            var entitity = await dbContext.PaymentTypes.Where(e => e.Id == id && e.IsActive == true).FirstOrDefaultAsync();

            return entitity;
        }

        public async Task DeletePaymentType(int Id)
        {
            var entity = await dbContext.PaymentTypes.FirstAsync(x => x.Id == Id);
            entity.IsActive = false;

            foreach (var payment in entity.Payments)
            {
                payment.IsActive = false;
            }

            await dbContext.SaveChangesAsync();
        }

    }
}
