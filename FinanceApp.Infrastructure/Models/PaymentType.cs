using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Infrastructure.Models
{
    /// <summary>
    /// A data model of a type of payment available
    /// </summary>
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<CurrentPayment> Payments { get; set; } = new List<CurrentPayment>();
    }
}
