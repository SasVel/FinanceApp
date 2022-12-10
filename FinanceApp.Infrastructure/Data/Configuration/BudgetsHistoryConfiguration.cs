using FinanceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Data.Configuration
{
    public class BudgetsHistoryConfiguration : IEntityTypeConfiguration<BudgetsHistory>
    {
        public void Configure(EntityTypeBuilder<BudgetsHistory> builder)
        {
            builder.HasData(CreateHistoryEntries());
        }

        private List<BudgetsHistory> CreateHistoryEntries()
        {
            List<BudgetsHistory> historyEntries = new List<BudgetsHistory>()
            {
                new BudgetsHistory()
                {
                    Id = 1,
                    EntryDate = DateTime.Parse("11/15/2022", CultureInfo.InvariantCulture),
                    Name = "Burgers",
                    Description = "Ingredients for the best burgers ever!",
                    Price = 30.50m,
                    IsSingular = true,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new BudgetsHistory()
                {
                    Id = 2,
                    EntryDate = DateTime.Parse("11/20/2022", CultureInfo.InvariantCulture),
                    Name = "Sushi",
                    Description = "Sushi with friends",
                    Price = 43.20m,
                    IsSingular = true,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new BudgetsHistory()
                {
                    Id = 3,
                    EntryDate = DateTime.Parse("11/22/2022", CultureInfo.InvariantCulture),
                    Name = "Mladya Chinar",
                    Description = "Bill",
                    Price = 60.00m,
                    IsSingular = true,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new BudgetsHistory()
                {
                    Id = 4,
                    EntryDate = DateTime.Parse("11/01/2022", CultureInfo.InvariantCulture),
                    Name = "Protein",
                    Description = "Tub of Protein",
                    Price = 40.00m,
                    IsSingular = false,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                }
            };
            return historyEntries;
        }
    }
}
