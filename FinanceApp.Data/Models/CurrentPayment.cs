using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FinanceApp.Infrastructure.DataConstants.CurrentBudget;

namespace FinanceApp.Infrastructure.Models
{
    /// <summary>
    /// A data model for a current payment in the monthly budget
    /// </summary>
    public class CurrentPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen)]
        public string Name { get; set; }

        [Required]
        [StringLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public bool IsSignular { get; set; }

        [Required]
        public bool IsPayedFor { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(PaymentType))]
        public int? PaymentTypeId { get; set; }

        public PaymentType? PaymentType { get; set; }

    }
}
