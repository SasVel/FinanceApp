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
    public class PaymentTypeServiceTests
    {
        private List<CurrentPayment> payments;
        private List<PaymentType> paymentTypes;
        private User user;
        private ApplicationDbContext dbContext;
        private IPaymentTypeService paymentTypeService;
        [SetUp]
        public void Setup()
        {
            //DbContext setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "FinanceDb") // Give a Unique name to the DB
                    .Options;
            this.dbContext = new ApplicationDbContext(options);

            //"54b87d10-2354-4185-a731-b73ec2d1d9cb"
            //User info setup
            var hasher = new PasswordHasher<User>();
            this.user = new User()
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

            this.dbContext.Add(user);
            this.dbContext.SaveChanges();

            //PaymentTypes setup
            this.paymentTypes = new List<PaymentType>()
            {
                new PaymentType()
                {
                    Name = "Food Payments",
                    IsActive = true,
                    UserId = dbContext.Users.First().Id
                },
                new PaymentType()
                {
                    Name = "Car Payments",
                    IsActive = true,
                    UserId = dbContext.Users.First().Id
                }
            };

            this.dbContext.AddRange(paymentTypes);
            this.dbContext.SaveChanges();

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
            this.dbContext.AddRange(this.payments);
            this.dbContext.SaveChanges();

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
                new Mock<PaymentTypeService>(this.dbContext, mockHttpAccessor, userManager);
            mockService.SetupProperty(x => x.userId, user.Id);

            this.paymentTypeService = mockService.Object;

        }

        [Test]
        public async Task AddPaymentTypeAsync_Test()
        {
            var paymentTypeToAdd = new PaymentType()
            {
                Name = "House Payments",
                IsActive = true,
                UserId = user.Id
            };
            await paymentTypeService.AddPaymentTypeAsync(paymentTypeToAdd);

            Assert.IsNotNull(dbContext.PaymentTypes.Where(pt => pt.Id == 3));
            Assert.That(dbContext.PaymentTypes.Where(pt => pt.Id == 3).Single().Name == paymentTypeToAdd.Name);
        }

        [Test]
        public async Task GetAllActivePaymentTypes_Test()
        {
            var entitiesFromService = await paymentTypeService.GetAllActivePaymentTypes();

            var entitiesFromDb = await dbContext.PaymentTypes
                .Include(p => p.Payments)
                .Where(p => p.IsActive == true && p.UserId == user.Id).ToArrayAsync();

            Assert.That(entitiesFromService, Is.EqualTo(entitiesFromDb));
        }

        [Test]
        public async Task GetAllInactivePaymentTypes_Test()
        {
            var entitiesFromService = await paymentTypeService.GetAllInactivePaymentTypes();

            var entitiesFromDb = await dbContext.PaymentTypes
                .Include(p => p.Payments)
                .Where(p => p.IsActive == false && p.UserId == user.Id).ToArrayAsync();

            Assert.That(entitiesFromService, Is.EqualTo(entitiesFromDb));
        }

        [Test]
        public async Task GetPaymentTypeAsync_Test()
        {
            var entityFromService = await paymentTypeService.GetPaymentTypeAsync(1);

            var entityFromDb = await dbContext.PaymentTypes
                .Where(e => e.Id == 1 && e.IsActive == true && e.UserId == user.Id).FirstOrDefaultAsync();

            Assert.That(entityFromService, Is.EqualTo(entityFromDb));
        }

        [Test]
        public async Task GetInactivePaymentTypeAsync()
        {
            var entityFromService = await paymentTypeService.GetInactivePaymentTypeAsync(1);

            var entityFromDb = await dbContext.PaymentTypes
                .Where(e => e.Id == 1 && e.IsActive == false && e.UserId == user.Id).FirstOrDefaultAsync();

            Assert.That(entityFromService, Is.EqualTo(entityFromDb));
        }

        [Test]
        public async Task DeletePaymentType_Test()
        {
            await paymentTypeService.DeletePaymentType(1);

            Assert.That(dbContext.PaymentTypes.First().IsActive == false);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
        }
    }
}