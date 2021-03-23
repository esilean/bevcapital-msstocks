using BevCapital.Stocks.Data.Context.Configs;
using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BevCapital.Stocks.Data.Context
{
    public class StocksContext : DbContext
    {
        public StocksContext(DbContextOptions<StocksContext> options)
                : base(options)
        { }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StockEntityMapping());
            modelBuilder.ApplyConfiguration(new StockPriceEntityMapping());
        }
    }
}
