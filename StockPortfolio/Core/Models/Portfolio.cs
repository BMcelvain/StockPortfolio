using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Core.Models
{
    public class Portfolio
    {
        public Guid id { get; set; }
        public Guid userID { get; set; }
        public string portfolioName { get; set; }
        public decimal balance { get; set; }
    }
}
