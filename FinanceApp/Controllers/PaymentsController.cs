using FinanceApp.Infrastructure.Models;
using FinanceApp.Models;
using FinanceApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FinanceApp.Controllers
{
    /// <summary>
    /// Controller for the Payments pages
    /// </summary>
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly IPaymentTypeService paymentTypeService;
        private readonly UserManager<User> userManager;

        public PaymentsController(IPaymentService _paymentService, IPaymentTypeService _paymentTypeService, UserManager<User> _userManager)
        {
            paymentService = _paymentService;
            paymentTypeService = _paymentTypeService;
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await paymentTypeService.GetAllActivePaymentTypes();
            var models = entities.Select(x => new PaymentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });

            return View(models);
        }

        public async Task<IActionResult> SelectPaymentType(int id)
        {
            var entity = await paymentTypeService.GetPaymentTypeAsync(id);
            if (entity != null)
            {
                var payments = await paymentService.GetAllActivePaymentsByTypeIdAsync(id);
                var model = new PaymentTypeViewModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CurrentPayments = payments
                        .Select(p => new PaymentViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Cost = p.Cost,
                            IsSignular = p.IsSignular,
                            IsPaidFor = p.IsPaidFor,
                        })
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home", 
                    new ErrorViewModel() 
                    { 
                        Message = "Payment type not found." 
                    } );
            }
            
        }
        [HttpGet]
        public IActionResult AddCurrentPayment(int id)
        {
            var model = new PaymentViewModel();
            model.PaymentTypeId = id;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCurrentPayment(PaymentViewModel model)
        {


            var currentUser = await userManager.GetUserAsync(this.User);
            var entry = new CurrentPayment()
            {
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                IsSignular = model.IsSignular,
                UserId = currentUser.Id,
                //TODO: Fix in the future. model.PaymentTypeId => model.Id for some reason
                PaymentTypeId = model.Id,
                IsActive = true,
                EntryDate = DateTime.Now,
            };

            await paymentService.AddCurrentPaymentAsync(entry);

            return RedirectToAction("SelectPaymentType", new { id = entry.PaymentTypeId });
        }

        [HttpGet]
        public IActionResult AddPaymentType()
        {
            var model = new PaymentTypeViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPaymentType(PaymentTypeViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var entry = new PaymentType()
            {
                Name = model.Name,
                IsActive = true,
                UserId = currentUser.Id
            };

            await paymentTypeService.AddPaymentTypeAsync(entry);
            var paymentTypes = await paymentTypeService.GetAllActivePaymentTypes();
            var id = paymentTypes.Single(pt => pt.Name == entry.Name).Id;
            return RedirectToAction("SelectPaymentType", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            await paymentTypeService.DeletePaymentType(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await paymentService.DeletePayment(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditPaymentType(int id)
        {
            var entity = await paymentTypeService.GetPaymentTypeAsync(id);
            var model = new PaymentTypeViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPaymentType(PaymentTypeViewModel model)
        {
            var entity = await paymentTypeService.GetPaymentTypeAsync(model.Id);
            entity.Name = model.Name;


            await paymentTypeService.SaveChangesToPaymentTypeAsync();

            return RedirectToAction("SelectPaymentType", new { id = model.Id });
        }


        public async Task<IActionResult> PayForPayment(int id)
        {
            await paymentService.PayForPayment(id);

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> UndoPayment(int id)
        {
            await paymentService.UndoPayment(id);

            return RedirectToAction("Index", "Dashboard");
        }

    }
}
