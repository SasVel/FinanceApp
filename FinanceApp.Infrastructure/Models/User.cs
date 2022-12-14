using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Infrastructure.Models
{
    /// <summary>
    /// A data model for the user
    /// </summary>
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "money")]
        [Precision(18,2)]
        public decimal MonthlyBudget { get; set; }

        public string Currency { get; set; }
    }
}
