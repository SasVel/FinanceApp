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
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasData(CreateTemplates());
        }

        private List<Template> CreateTemplates()
        {
            var templates = new List<Template>()
            {
                new Template()
                {
                    Id = 1,
                    Name = "Waffle",
                    Description = "Tasty waffle",
                    Cost = 1m,
                    Quantity = 0,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb"
                }
            };
            return templates;
        }
    }
}
