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
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasData(CreatePaymentTypes());
        }

        private List<PaymentType> CreatePaymentTypes()
        {
            var paymentTypes = new List<PaymentType>()
            {
                new PaymentType()
                {
                    Id = 1,
                    Name = "Food Payments",
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb"
                },
                new PaymentType()
                {
                    Id = 2,
                    Name = "Car Payments",
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb"
                }
            };
            return paymentTypes;
        }
    }
}
