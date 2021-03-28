using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BevCapital.Stocks.Data.Migrations
{
    public partial class Stock_InitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks_AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    RowVersion = table.Column<DateTime>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_Stocks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    RowVersion = table.Column<DateTime>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    StockName = table.Column<string>(maxLength: 100, nullable: false),
                    Exchange = table.Column<string>(maxLength: 100, nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_AppUserStocks",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    StockId = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_AppUserStocks", x => new { x.AppUserId, x.StockId });
                    table.ForeignKey(
                        name: "FK_Stocks_AppUserStocks_Stocks_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Stocks_AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_AppUserStocks_Stocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks_Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_StockPrices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    RowVersion = table.Column<DateTime>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Open = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    LatestPrice = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    LatestPriceTime = table.Column<DateTime>(nullable: false),
                    DelayedPrice = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    DelayedPriceTime = table.Column<DateTime>(nullable: false),
                    PreviousClosePrice = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    ChangePercent = table.Column<decimal>(type: "decimal(10,5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_StockPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_StockPrices_Stocks_Stocks_Id",
                        column: x => x.Id,
                        principalTable: "Stocks_Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AppUserStocks_StockId",
                table: "Stocks_AppUserStocks",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks_AppUserStocks");

            migrationBuilder.DropTable(
                name: "Stocks_StockPrices");

            migrationBuilder.DropTable(
                name: "Stocks_AppUsers");

            migrationBuilder.DropTable(
                name: "Stocks_Stocks");
        }
    }
}
