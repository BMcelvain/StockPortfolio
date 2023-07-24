using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Api.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
