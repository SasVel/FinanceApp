using FinanceApp.Core.Contracts;
using FinanceApp.Core.Services;
using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FinanceApp.UnitTests
{
    public class PaymentServiceTests
    {
        private List<CurrentPayment> payments;
        private User user;
        private ApplicationDbContext dbContext;
        private IPaymentService paymentService;
        [SetUp]
        public void Setup()
        {
            //User info setup
            var hasher = new PasswordHasher<User>();
            this.user = new User()
            {
                Id = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                UserName = "guest_user",
                NormalizedUserName = "guest_user".Normalize(),
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com".Normalize(),
                MonthlyBudget = 3000
            };
            user.PasswordHash =
                 hasher.HashPassword(user, "guest123");


            //Payments info setup
            this.payments = new List<CurrentPayment>()
            {
                new CurrentPayment()
                {
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
                    Name = "Bubble Tea",
                    Description = "Bubble tea order.",
                    EntryDate = DateTime.Now.AddDays(-2),
                    Cost = 7.00m,
                    IsSignular = true,
                    IsPaidFor = true,
                    IsActive = false,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                },
                new CurrentPayment()
                {
                    Name = "Meal Prep",
                    Description = "Meal prep for the week.",
                    EntryDate = DateTime.Now.AddDays(-5),
                    Cost = 40.00m,
                    IsSignular = false,
                    IsPaidFor = true,
                    IsActive = true,
                    UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                    PaymentTypeId = 1
                }
            };

            //DbContext setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "FinanceDb") // Give a Unique name to the DB
                    .Options;

            this.dbContext = new ApplicationDbContext(options);
            this.dbContext.Add(user);
            this.dbContext.AddRange(this.payments);
            this.dbContext.SaveChanges();

            var userManager = MockHelpers.CreateUserManager<User>();
            var httpContextAccessor = new Mock<HttpContextAccessor>();
            IPaymentService service =
                new PaymentService(this.dbContext, httpContextAccessor.Object, userManager);
            this.paymentService = service;

        }
        [Test]
        public async Task GetPaymentAsync_Test()
        {
            var paymentToGet = await dbContext.CurrentPayments.FirstAsync();

            var paymentGot = await paymentService.GetPaymentAsync(1);

            Assert.IsNotNull(paymentGot);
            Assert.That(paymentGot, Is.EqualTo(paymentToGet));
        }


        [Test]
        public async Task GetAllCurrentPayments_Test()
        {
            var paymentsGet = await paymentService.GetAllCurrentPayments();
            
            Assert.That(paymentsGet, Is.EqualTo(dbContext.CurrentPayments));
        }

        [Test]
        public async Task GetCurrentPaymentsSingular_Test()
        {
            var paymentsToGetTrue = await dbContext.CurrentPayments.Where(p => p.IsSignular == true).ToArrayAsync();
            var paymentsGotTrue = await paymentService.GetCurrentPaymentsSingular(true);

            var paymentsToGetFalse = await dbContext.CurrentPayments.Where(p => p.IsSignular == false).ToArrayAsync();
            var paymentsGotFalse = await paymentService.GetCurrentPaymentsSingular(false);

            Assert.That(paymentsToGetTrue, Is.EqualTo(paymentsGotTrue));
            Assert.That(paymentsToGetFalse, Is.EqualTo(paymentsGotFalse));
        }

        [Test]
        public async Task GetCurrentPaymentsIsPaidFor_Test()
        {
            var paymentsToGetTrue = await dbContext.CurrentPayments.Where(p => p.IsPaidFor == true).ToArrayAsync();
            var paymentsGotTrue = await paymentService.GetCurrentPaymentsIsPaidFor(true);

            var paymentsToGetFalse = await dbContext.CurrentPayments.Where(p => p.IsPaidFor == false).ToArrayAsync();
            var paymentsGotFalse = await paymentService.GetCurrentPaymentsIsPaidFor(false);

            Assert.That(paymentsToGetTrue, Is.EqualTo(paymentsGotTrue));
            Assert.That(paymentsToGetFalse, Is.EqualTo(paymentsGotFalse));
        }

        [Test]
        public async Task AddCurrentPaymentAsync_Test()
        {

            var paymentToAdd = new CurrentPayment()
            {
                Name = "Tacos",
                Description = "Tacossss",
                EntryDate = DateTime.Now.AddDays(-3),
                Cost = 130.00m,
                IsSignular = true,
                IsPaidFor = true,
                IsActive = true,
                UserId = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                PaymentTypeId = 1
            };

            this.payments.Add(paymentToAdd);
            await paymentService.AddCurrentPaymentAsync(paymentToAdd);

            Assert.That(this.payments, Is.EqualTo(this.dbContext.CurrentPayments));
        }

        [Test]
        public async Task GetUsersFullMonthlyBudget_Test()
        {
            var userBudget = await paymentService.GetUsersFullMonthlyBudget();

            Assert.That(userBudget, Is.EqualTo(user.MonthlyBudget));
        }
        

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Dispose();
        }
    }
}