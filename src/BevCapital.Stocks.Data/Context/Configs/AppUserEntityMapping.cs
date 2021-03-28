using BevCapital.Stocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BevCapital.Stocks.Data.Context.Configs
{
    public class AppUserEntityMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Stocks_AppUsers");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(e => e.CreatedAtUtc)
                   .ValueGeneratedOnAdd()
                   .IsRequired();
            builder.Property(e => e.UpdatedAtUtc)
                   .ValueGeneratedOnAddOrUpdate()
                   .IsRequired();

            builder.Property(e => e.RowVersion)
                   .IsRowVersion();

            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.Invalid);
            builder.Ignore(e => e.ValidationResult);

        }
    }
}
