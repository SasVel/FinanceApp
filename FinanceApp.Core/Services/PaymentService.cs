using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using FinanceApp.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Core.Services
{
    /// <summary>
    /// A service for all actions connected to payments
    /// </summary>
    public class PaymentService : IPaymentService
    {
        public readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public virtual string userId { get; set; }

        public PaymentService(ApplicationDbContext _dbContext, IHttpContextAccessor _httpContextAccessor, UserManager<User> _userManager)
        {
            dbContext = _dbContext;
            httpContextAccessor = _httpContextAccessor;
            userManager = _userManager;

            if (this.userId == null)
                this.userId = userManager.GetUserId(httpContextAccessor.HttpContext?.User);
        }

        public async Task AddCurrentPaymentAsync(CurrentPayment entry)
        {
            await dbContext.CurrentPayments.AddAsync(entry);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurrentPayment>> GetAllCurrentPayments()
        {
            var entities = await dbContext.CurrentPayments.ToArrayAsync();

            return entities;
        }

        public async Task<CurrentPayment> GetPaymentAsync(int id)
        {
            var entity = await dbContext.CurrentPayments.Where(p => p.Id == id).FirstAsync();

            return entity;
        }
        
        public async Task<IEnumerable<CurrentPayment>> GetAllActivePaymentsByTypeIdAsync(int PaymentTypeId)
        {
            var entities = await dbContext.CurrentPayments.Where(p => p.PaymentTypeId == PaymentTypeId && p.IsActive == true).ToArrayAsync();

            return entities;
        }
        
        public async Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsIsPaidFor(bool isPaid)
        {
            if (isPaid)
            {
                return await dbContext.CurrentPayments
                    .Where(p => p.IsPaidFor == true).ToArrayAsync();
            }
            else
            {
                return await dbContext.CurrentPayments
                    .Where(p => p.IsPaidFor == false).ToArrayAsync();
            }
        }
        
        public async Task<IEnumerable<CurrentPayment>> GetCurrentPaymentsSingular(bool isSingular)
        {
            if (isSingular)
            {
                return await dbContext.CurrentPayments
                    .Where(p => p.IsSignular == true).ToArrayAsync();
            }
            else
            {
                return await dbContext.CurrentPayments
                    .Where(p => p.IsSignular == false).ToArrayAsync();
            }
        }
        
        public async Task<decimal?> GetUsersCurrentBudget()
        {
            var fullBudget = await GetUsersFullMonthlyBudget();
            var donePaymentsSum = await dbContext.CurrentPayments
                .Where(p => p.IsPaidFor == true && p.UserId == userId)
                .Select(p => p.Cost).SumAsync();

            return fullBudget - donePaymentsSum;
        }

        public async Task<decimal?> GetUsersEstimatedBudget()
        {
            var fullBudget = await GetUsersFullMonthlyBudget();
            var allPaymentsSum = await dbContext.CurrentPayments
                .Where(p => p.IsActive == true && p.UserId == userId)
                .Select(p => p.Cost)
                .SumAsync();

            return fullBudget - allPaymentsSum;
        }

        public async Task<decimal?> GetUsersFullMonthlyBudget()
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var budget = user?.MonthlyBudget;
            return budget;
        }

        public async Task PayForPayment(int id)
        {
            var payment = await dbContext.CurrentPayments.FirstAsync(p => p.Id == id);
            payment.IsPaidFor = true;

            await dbContext.SaveChangesAsync();
        }

        
        public async Task UndoPayment(int id)
        {
            var payment = await dbContext.CurrentPayments.FirstAsync(p => p.Id == id);
            payment.IsPaidFor = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task SetUsersMonthlyBudget(decimal newBudget)
        {
            var user = await dbContext.Users
                .FirstAsync(u => u.Id == userId);
            user.MonthlyBudget = newBudget;

            await dbContext.SaveChangesAsync();

        }

        public async Task DeletePayment(int id)
        {
            var entry = await dbContext.CurrentPayments
                .FirstAsync(p => p.Id == id);
            entry.IsActive = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task SetUsersCurrency(string Ccy)
        {
            var user = await dbContext.Users
               .FirstAsync(u => u.Id == userId);
            user.Currency = Ccy;

            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUsersCurrency()
        {
            var user = await dbContext.Users
               .FirstAsync(u => u.Id == userId);
            var ccy = user.Currency;

            return ccy;
        }
    }
}
