﻿using FinanceApp.Infrastructure;
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
                return await dbContext.CurrentPayments.Where(p => p.IsPaidFor == true).ToArrayAsync();
            }
            else
            {
                return await dbContext.CurrentPayments.Where(p => p.IsPaidFor == false).ToArrayAsync();
            }
        }
        
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
        
        public async Task<decimal?> GetUsersCurrentBudget(string id)
        {
            var fullBudget = await GetUsersFullMonthlyBudget(id);
            var donePaymentsSum = await dbContext.CurrentPayments.Where(p => p.IsPaidFor == true).Select(p => p.Cost).SumAsync();

            return fullBudget - donePaymentsSum;
        }

        public async Task<decimal?> GetUsersEstimatedBudget(string id)
        {
            var fullBudget = await GetUsersFullMonthlyBudget(id);
            var allPaymentsSum = await dbContext.CurrentPayments
                .Where(p => p.IsActive == true)
                .Select(p => p.Cost)
                .SumAsync();

            return fullBudget - allPaymentsSum;
        }

        public async Task<decimal?> GetUsersFullMonthlyBudget(string id)
        {
            var user = await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
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

        public async Task SetUsersMonthlyBudget(string id, decimal newBudget)
        {
            var user = await dbContext.Users.Where(u => u.Id == id).FirstAsync();
            user.MonthlyBudget = newBudget;

            await dbContext.SaveChangesAsync();

        }

        public async Task DeletePayment(int id)
        {
            var entry = dbContext.CurrentPayments.FirstOrDefault(p => p.Id == id);
            entry.IsActive = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
