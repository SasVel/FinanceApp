using FinanceApp.Core.Contracts;
using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public virtual string userId { get; set; }

        public PaymentTypeService(ApplicationDbContext _dbContext, IHttpContextAccessor _httpContextAccessor, UserManager<User> _userManager)
        {
            dbContext = _dbContext;
            httpContextAccessor = _httpContextAccessor;
            userManager = _userManager;

            if (userId == null)
                this.userId = userManager.GetUserId(httpContextAccessor.HttpContext?.User);
        }

        public async Task AddPaymentTypeAsync(PaymentType entry)
        {
            await dbContext.PaymentTypes.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentType?>> GetAllActivePaymentTypes()
        {
            var entities = await dbContext.PaymentTypes
                .Include(p => p.Payments)
                .Where(p => p.IsActive == true && p.UserId == userId).ToArrayAsync();

            return entities;
        }

        public async Task<IEnumerable<PaymentType?>> GetAllInactivePaymentTypes()
        {
            var entities = await dbContext.PaymentTypes
                .Include(p => p.Payments)
                .Where(p => p.IsActive == false && p.UserId == userId).ToArrayAsync();

            return entities;
        }

        public async Task<PaymentType?> GetPaymentTypeAsync(int id)
        {
            var entity = await dbContext.PaymentTypes
                .Where(e => e.Id == id && e.IsActive == true && e.UserId == userId).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<PaymentType> GetInactivePaymentTypeAsync(int id)
        {
            var entity = await dbContext.PaymentTypes
                .Where(e => e.Id == id && e.IsActive == false && e.UserId == userId).FirstOrDefaultAsync();

            return entity;
        }

        public async Task DeletePaymentType(int Id)
        {
            var entity = await dbContext.PaymentTypes
                .FirstOrDefaultAsync(x => x.Id == Id && x.UserId == userId);
            if (entity != null)
            {
                entity.IsActive = false;

                foreach (var payment in entity.Payments)
                {
                    payment.IsActive = false;
                }
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveChangesToPaymentTypeAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
