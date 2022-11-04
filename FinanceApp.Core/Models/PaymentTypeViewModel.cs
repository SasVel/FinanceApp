using System.ComponentModel.DataAnnotations;
using static FinanceApp.Data.DataConstants.PaymentType;

namespace FinanceApp.Models
{
    public class PaymentTypeViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen)]
        public string Name { get; set; }
    }
}
