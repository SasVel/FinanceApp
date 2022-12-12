using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        public List<CurrentPayment> Payments { get; set; } = new List<CurrentPayment>();
    }
}
