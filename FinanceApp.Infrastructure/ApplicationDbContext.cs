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

        public DbSet<BudgetsHistory> BudgetsHistory { get; set; }

        public DbSet<CurrentPayment> CurrentPayments { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Template> Templates { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<PaymentType>().HasData(new PaymentType()
            {
                Id = 1,
                Name = "General"
            });

            base.OnModelCreating(builder);
        }
    }
}