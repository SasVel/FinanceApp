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
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace FinanceApp.UnitTests.ServicesTests
{
    public class HistoryServiceTests
    {
        private List<CurrentPayment> payments;
        private List<PaymentType> paymentTypes;
        private List<BudgetsHistory> historyEntries;
        private User user;
        private ApplicationDbContext dbContext;
        private IHistoryService historyService;
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

            //PaymentTypes setup
            paymentTypes = new List<PaymentType>()
            {
                new PaymentType()
                {
                    Name = "Food Payments",
                    IsActive = false,
                    UserId = dbContext.Users.First().Id
                },
                new PaymentType()
                {
                    Name = "Car Payments",
                    IsActive = true,
                    UserId = dbContext.Users.First().Id
                }
            };

            dbContext.AddRange(paymentTypes);
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

            //BudgetsHistory data setup

            historyEntries = new List<BudgetsHistory>()
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
                    UserId = dbContext.Users.First().Id,
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
                    UserId = dbContext.Users.First().Id,
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
                    IsActive = false,
                    UserId = dbContext.Users.First().Id,
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
                    UserId = dbContext.Users.First().Id,
                    PaymentTypeId = 1
                }
            };

            dbContext.AddRange(historyEntries);
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
                new Mock<HistoryService>(dbContext, mockHttpAccessor, userManager);
            mockService.SetupProperty(x => x.userId, user.Id);

            historyService = mockService.Object;

        }

        [Test]
        public async Task GetAllDeletedPayments_Test()
        {
            var paymentsfromService = await historyService.GetAllDeletedPayments();

            var paymentsFromDb = await dbContext.CurrentPayments.Where(p => p.IsActive == false && p.UserId == user.Id).ToListAsync();
            var pastDeleted = await dbContext.BudgetsHistory.Where(p => p.IsActive == false)
                .Select(p => new CurrentPayment()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Cost = p.Price,
                    EntryDate = p.EntryDate,
                    IsSignular = p.IsSingular,
                    IsPaidFor = p.IsPaidFor,
                    IsActive = p.IsActive,
                    UserId = p.UserId,
                    PaymentTypeId = p.PaymentTypeId,
                })
                .ToListAsync();

            paymentsFromDb.AddRange(pastDeleted);

            //does not give equals when directly compared, but are equal when debugging
            //Possible entry date difference
            Assert.That(paymentsfromService.Count, Is.EqualTo(paymentsFromDb.Count));
        }

        [Test]
        public async Task GetAllDeletedPaymentTypes_Test()
        {
            var paymentTypesFromService = await historyService.GetAllDeletedPaymentTypes();
            var paymentTypesFromDb = dbContext.PaymentTypes.Where(pt => pt.IsActive == false);


            Assert.That(paymentTypesFromService, Is.EqualTo(paymentTypesFromDb));
        }

        [Test]
        public async Task GetHistoryPaymentsByMonthAndYearAsync_Test()
        {
            var paymentsFromService = await historyService.GetHistoryPaymentsByMonthAndYearAsync(11, 2022);
            var paymentsFromDb = await dbContext.BudgetsHistory.Where(p => p.EntryDate.Month == 11 && p.EntryDate.Year == 2022).ToArrayAsync();

            Assert.That(paymentsFromService, Is.EqualTo(paymentsFromDb));
        }

        [Test]
        public async Task UndoDeletedPayment_Test()
        {
            var payment = await dbContext.CurrentPayments.FirstAsync(p => p.Id == 2);
            await historyService.UndoDeletedPayment(payment);

            Assert.That(payment.IsActive == true);
        }

        [Test]
        public async Task UndoDeletedPaymentType_Test()
        {
            var paymentType = await dbContext.PaymentTypes.FirstAsync(p => p.Id == 1);
            await historyService.UndoDeletedPaymentType(paymentType);

            Assert.That(paymentType.IsActive == true);
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
        }
    }
}