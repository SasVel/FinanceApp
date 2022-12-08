using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Infrastructure.Models
{
    /// <summary>
    /// A data model for the history of monthly budgets
    /// </summary>
    public class BudgetsHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime EntryDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(PaymentType))]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
