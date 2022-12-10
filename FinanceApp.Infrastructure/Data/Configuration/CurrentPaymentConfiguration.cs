using FinanceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Data.Configuration
{
    public class CurrentPaymentConfiguration : IEntityTypeConfiguration<CurrentPayment>
    {
        public void Configure(EntityTypeBuilder<CurrentPayment> builder)
        {
            builder.HasData(CreatePayments());
        }

        private List<CurrentPayment> CreatePayments()
        {
            var payments = new List<CurrentPayment>()
            {
                new CurrentPayment()
                {
                    Id = 1,
                    Name = "Pizza",
                    Description = "Pizza takeout.",
                    EntryDate = DateTime.Now,
                    Cost = 16.00m,
                    IsSignular = true,
                    IsPaidFor = false,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new CurrentPayment()
                {
                    Id = 2,
                    Name = "Bubble Tea",
                    Description = "Bubble tea order.",
                    EntryDate = DateTime.Now.AddDays(-2),
                    Cost = 7.00m,
                    IsSignular = true,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new CurrentPayment()
                {
                    Id = 3,
                    Name = "Meal Prep",
                    Description = "Meal prep for the week.",
                    EntryDate = DateTime.Now.AddDays(-5),
                    Cost = 40.00m,
                    IsSignular = true,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                }
            };
            return payments;
        }
    }
}
