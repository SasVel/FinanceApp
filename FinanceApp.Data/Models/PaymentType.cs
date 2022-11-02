using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Data.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CurrentPayment> Payments { get; set; } = new List<CurrentPayment>();
    }
}
