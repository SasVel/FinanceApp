using FinanceApp.Core.Contracts;
using FinanceApp.Core.Services;
using FinanceApp.Infrastructure;
using FinanceApp.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace FinanceApp.UnitTests.ServicesTests
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
            //DbContext setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "FinanceDb") // Give a Unique name to the DB
                    .Options;
            dbContext = new ApplicationDbContext(options);

            //"54b87d10-2354-4185-a731-b73ec2d1d9cb"
            //User info setup
            var hasher = new PasswordHasher<User>();
            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "guest_user",
                NormalizedUserName = "guest_user".Normalize(),
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com".Normalize(),
                MonthlyBudget = 3000,
                Currency = "BGN"
            };
            user.PasswordHash =
                 hasher.HashPassword(user, "guest123");

            dbContext.Add(user);
            dbContext.SaveChanges();

            //Payments info setup
            payments = new List<CurrentPayment>()
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
                    UserId = dbContext.Users.First().Id,
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
                    UserId = dbContext.Users.First().Id,
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
                    UserId = dbContext.Users.First().Id,
                    PaymentTypeId = 1
                }
            };
            dbContext.AddRange(payments);
            dbContext.SaveChanges();

            var userManager = MockHelpers.CreateUserManager<User>();
            //var userManager = Substitute.For<UserManager<User>>();

            //userManager.Options.ClaimsIdentity
            var mockIdentity = new GenericIdentity("guest_user");
            var contextUser = new ClaimsPrincipal(mockIdentity);
            var mockHttpAccessor = Substitute.For<IHttpContextAccessor>();
            var context = new DefaultHttpContext
            {
                User = contextUser,
                Connection =
                {
                    Id = Guid.NewGuid().ToString()
                }
            };

            var mockService =
                new Mock<PaymentService>(dbContext, mockHttpAccessor, userManager);
            mockService.SetupProperty(x => x.userId, user.Id);

            paymentService = mockService.Object;

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

            payments.Add(paymentToAdd);
            await paymentService.AddCurrentPaymentAsync(paymentToAdd);

            Assert.That(payments, Is.EqualTo(dbContext.CurrentPayments));
        }

        [Test]
        public async Task GetUsersFullMonthlyBudget_Test()
        {
            var userBudget = await paymentService.GetUsersFullMonthlyBudget();

            Assert.That((decimal)userBudget, Is.EqualTo(user.MonthlyBudget));
        }

        [Test]
        public async Task GetUsersCurrentBudget_Test()
        {
            var currentBudget = await paymentService.GetUsersCurrentBudget();

            Assert.That((decimal)currentBudget, Is.EqualTo(2953.00m));
        }

        [Test]
        public async Task GetUsersEstimatedBudget_Test()
        {
            var currentBudget = await paymentService.GetUsersEstimatedBudget();

            Assert.That((decimal)currentBudget, Is.EqualTo(2944.00m));
        }

        [Test]
        public async Task PayForPayment_Test()
        {
            await paymentService.PayForPayment(1);
            var entity = await dbContext.CurrentPayments.FirstAsync();

            Assert.That(entity.IsPaidFor == true);
        }

        [Test]
        public async Task UndoPayment_Test()
        {
            await paymentService.UndoPayment(2);
            var entity = await dbContext.CurrentPayments.FirstAsync(p => p.Id == 2);

            Assert.That(entity.IsPaidFor == false);
        }
        [Test]
        public async Task SetUsersMonthlyBudget_Test()
        {
            await paymentService.SetUsersMonthlyBudget(5000m);

            Assert.That(dbContext.Users.First().MonthlyBudget == 5000m);
        }

        [Test]
        public async Task DeletePayment_Test()
        {
            await paymentService.DeletePayment(1);

            Assert.That(dbContext.CurrentPayments.First().IsActive == false);
        }

        [Test]
        public async Task SetUsersCurrency_Test()
        {
            await paymentService.SetUsersCurrency("USD");

            Assert.That(dbContext.Users.First().Currency == "USD");
        }

        [Test]
        public async Task GetUsersCurrency_Test()
        {
            var ccy = await paymentService.GetUsersCurrency();

            Assert.That(dbContext.Users.First().Currency == ccy);
        }

        [TearDown]
        public void TearDown()
        {
            //this.dbContext.Dispose();
            dbContext.Database.EnsureDeleted();
        }
    }
}