// <auto-generated />
using System;
using BevCapital.Stocks.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BevCapital.Stocks.Data.Migrations
{
    [DbContext(typeof(StockContext))]
    partial class StockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Stocks_AppUsers");
                });

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.AppUserStock", b =>
                {
                    b.Property<Guid>("AppUserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("StockId")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.HasKey("AppUserId", "StockId");

                    b.HasIndex("StockId");

                    b.ToTable("Stocks_AppUserStocks");
                });

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.Stock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedAtUtc")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Stocks_Stocks");
                });

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.StockPrice", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<decimal?>("ChangePercent")
                        .HasColumnType("decimal(10,5)");

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(10,5)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("DelayedPrice")
                        .HasColumnType("decimal(10,5)");

                    b.Property<DateTime>("DelayedPriceTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("High")
                        .HasColumnType("decimal(10,5)");

                    b.Property<decimal>("LatestPrice")
                        .HasColumnType("decimal(10,5)");

                    b.Property<DateTime>("LatestPriceTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Low")
                        .HasColumnType("decimal(10,5)");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(10,5)");

                    b.Property<decimal>("PreviousClosePrice")
                        .HasColumnType("decimal(10,5)");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Stocks_StockPrices");
                });

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.AppUserStock", b =>
                {
                    b.HasOne("BevCapital.Stocks.Domain.Entities.AppUser", "AppUser")
                        .WithMany("AppUserStocks")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BevCapital.Stocks.Domain.Entities.Stock", "Stock")
                        .WithMany("AppUserStocks")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BevCapital.Stocks.Domain.Entities.StockPrice", b =>
                {
                    b.HasOne("BevCapital.Stocks.Domain.Entities.Stock", "Stock")
                        .WithOne("StockPrice")
                        .HasForeignKey("BevCapital.Stocks.Domain.Entities.StockPrice", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
