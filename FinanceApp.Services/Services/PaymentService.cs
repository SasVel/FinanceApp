using FinanceApp.Data;
using FinanceApp.Data.Models;
using FinanceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Services.Services
{
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
