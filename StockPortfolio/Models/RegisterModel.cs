using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Api.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is requird.")]
        public string Password { get; set; }
    }
}
