using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BevCapital.Stocks.Data.Context.Configs
{
    public class StockEntityMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks_Stocks");
            builder.HasKey(r => r.Id);

            builder.Property(e => e.Id)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.StockName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Exchange)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Website)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.CreatedAtUtc)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(e => e.UpdatedAtUtc)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.HasOne<StockPrice>(e => e.StockPrice)
                   .WithOne(x => x.Stock)
                   .HasForeignKey<StockPrice>(x => x.Id);

            builder.Property(e => e.RowVersion)
                .IsRowVersion();

            builder.Ignore(e => e.Version);
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.Invalid);
            builder.Ignore(e => e.ValidationResult);
        }
    }
}
