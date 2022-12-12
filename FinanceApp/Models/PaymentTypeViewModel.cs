using System.ComponentModel.DataAnnotations;
using static FinanceApp.Infrastructure.DataConstants.PaymentType;

namespace FinanceApp.Models
{
    /// <summary>
    /// A view model for payment types
    /// </summary>
    public class PaymentTypeViewModel
    {
        public int Id { get; set; }

        [StringLength(NameMaxLen, MinimumLength = NameMinLen)]
        public string Name { get; set; }

        public IEnumerable<PaymentViewModel>? CurrentPayments { get; set; }
    }
}
