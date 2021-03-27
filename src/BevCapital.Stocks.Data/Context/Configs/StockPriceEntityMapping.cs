using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BevCapital.Stocks.Data.Context.Configs
{
    public class StockPriceEntityMapping : IEntityTypeConfiguration<StockPrice>
    {
        public void Configure(EntityTypeBuilder<StockPrice> builder)
        {
            builder.ToTable("Stocks_StockPrices");
            builder.HasKey(r => r.Symbol);

            builder.Property(e => e.Symbol)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.Open)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.Close)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.High)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.Low)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.LatestPrice)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.LatestPriceTime)
                .IsRequired();
            builder.Property(e => e.DelayedPrice)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.DelayedPriceTime)
                .IsRequired();
            builder.Property(e => e.PreviousClosePrice)
                .HasColumnType<decimal>("decimal(10,5)")
                .IsRequired();
            builder.Property(e => e.ChangePercent)
                .HasColumnType<decimal?>("decimal(10,5)");
            builder.Property(e => e.CreatedAt)
                .IsRequired();
            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            builder.HasOne(e => e.Stock)
                   .WithOne(x => x.StockPrice)
                   .IsRequired();

            builder.Property(e => e.RowVersion)
                .IsRowVersion();

            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.Invalid);
            builder.Ignore(e => e.ValidationResult);
        }
    }
}
