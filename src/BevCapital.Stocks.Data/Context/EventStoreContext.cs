using BevCapital.Stocks.Domain.Core.EventStores;
using Microsoft.EntityFrameworkCore;

namespace BevCapital.Stocks.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options)
                : base(options)
        {
        }

        public DbSet<StreamState> Streams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StreamState>(b =>
            {
                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.CreatedAtUtc)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                b.Property(p => p.AggregateId)
                    .HasMaxLength(36)
                    .IsRequired();

                b.Property(p => p.Type)
                    .IsRequired();

                b.Property(p => p.Data)
                    .IsRequired();

                b.HasIndex(k => new { k.AggregateId, k.Version });

                b.HasKey(k => k.Id);

                b.ToTable("Stocks_EventsStore");
            });
        }
    }
}
