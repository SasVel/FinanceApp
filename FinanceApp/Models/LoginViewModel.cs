using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    /// <summary>
    /// A view model for logging in
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
