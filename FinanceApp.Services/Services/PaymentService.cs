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

        public async Task AddPaymentTypeAsync(PaymentType model)
        {
            var entity = new PaymentType()
            {
                Name = model.Name
            };

            await dbContext.PaymentTypes.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypes()
        {
            var entities = await dbContext.PaymentTypes.ToArrayAsync();

            return entities;
        }
    }
}
