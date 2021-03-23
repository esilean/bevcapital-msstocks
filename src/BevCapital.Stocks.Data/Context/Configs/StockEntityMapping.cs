using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BevCapital.Stocks.Data.Context.Configs
{
    public class StockEntityMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(r => r.Symbol);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Exchange)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Website)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.CreatedAt)
                .IsRequired();
            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            builder.HasOne<StockPrice>(e => e.StockPrice)
                   .WithOne(x => x.Stock)
                   .HasForeignKey<StockPrice>(x => x.Symbol);

            builder.Property(e => e.RowVersion)
                .IsRowVersion();

            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.Invalid);
            builder.Ignore(e => e.ValidationResult);
        }
    }
}
