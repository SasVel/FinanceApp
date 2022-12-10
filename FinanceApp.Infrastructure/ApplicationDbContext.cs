using FinanceApp.Infrastructure.Data.Configuration;
using FinanceApp.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infrastructure
{
    /// <summary>
    /// The main database context in the application. Establishes connection to the database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {



        }

        public DbSet<User> Users { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<CurrentPayment> CurrentPayments { get; set; }

        public DbSet<BudgetsHistory> BudgetsHistory { get; set; }

        public DbSet<Template> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new TemplateConfiguration());
            builder.ApplyConfiguration(new PaymentTypeConfiguration());
            builder.ApplyConfiguration(new BudgetsHistoryConfiguration());
            builder.ApplyConfiguration(new CurrentPaymentConfiguration());

            base.OnModelCreating(builder);
        }
    }
}