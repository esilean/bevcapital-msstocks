using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BevCapital.Stocks.Data.Context.Configs
{
    public class AppUserStockEntityMapping : IEntityTypeConfiguration<AppUserStock>
    {
        public void Configure(EntityTypeBuilder<AppUserStock> builder)
        {
            builder.ToTable("Stocks_AppUserStocks");

            builder.HasKey(ua =>
                new
                {
                    ua.AppUserId,
                    ua.StockId
                });

            builder.Property(x => x.AppUserId)
                   .IsRequired();
            builder.Property(x => x.StockId)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasOne(u => u.AppUser)
                   .WithMany(a => a.AppUserStocks)
                   .HasForeignKey(u => u.AppUserId);

            builder.HasOne(a => a.Stock)
                   .WithMany(u => u.AppUserStocks)
                   .HasForeignKey(a => a.StockId);
        }
    }
}
