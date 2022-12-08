using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    /// <summary>
    /// A view model for a payment for the current month
    /// </summary>
    public class CurrentPaymentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public bool IsSignular { get; set; }

        public bool IsPaidFor { get; set; }

        public int? PaymentTypeId { get; set; }
    }
}
