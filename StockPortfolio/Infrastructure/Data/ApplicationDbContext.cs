using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Stock>().HasKey(s => new { s.StockSymbol, s.UpdateDate });
            builder.Entity<Transaction>().HasKey(t => new { t.TransactionId});
            builder.Entity<Portfolio>().HasKey(p => new { p.PortfolioId });
        }

        public DbSet<Portfolio>? Portfolios { get; set; }
        public DbSet<Stock>? Stocks { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }
    }
}
