using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Core.Models
{
    public class Portfolio
    {
        public Guid PortfolioId { get; set; }
        public string UserId { get; set; }
        public string PortfolioName { get; set; }
        public decimal Balance { get; set; }
    }
}
