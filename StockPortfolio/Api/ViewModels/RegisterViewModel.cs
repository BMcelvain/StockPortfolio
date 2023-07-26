using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Api.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        [MinLength(5)]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requird.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
