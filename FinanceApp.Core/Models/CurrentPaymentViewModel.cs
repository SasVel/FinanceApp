using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    /// <summary>
    /// A view model for a payment for the current month
    /// </summary>
    public class CurrentPaymentViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public bool IsSignular { get; set; }

        [Required]
        public bool IsPayedFor { get; set; }

        public int? PaymentTypeId { get; set; }
    }
}
