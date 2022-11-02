using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FinanceApp.Data.DataConstants.CurrentBudget;

namespace FinanceApp.Data.Models
{
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
