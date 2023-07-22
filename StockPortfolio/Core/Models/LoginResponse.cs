namespace StockPortfolio.Core.Models
{
    public class LoginResponse
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
