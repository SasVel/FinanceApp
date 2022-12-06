using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Infrastructure.Models
{
    /// <summary>
    /// A data model for a template for saving singular payments
    /// </summary>
    public class Template
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
        public int Quantity { get; set; }
    }
}
