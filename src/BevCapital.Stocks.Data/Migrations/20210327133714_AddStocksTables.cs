using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BevCapital.Stocks.Data.Migrations
{
    public partial class AddStocksTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks_AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<DateTime>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_Stocks",
                columns: table => new
                {
                    Symbol = table.Column<string>(maxLength: 20, nullable: false),
                    RowVersion = table.Column<DateTime>(rowVersion: true, nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Exchange = table.Column<string>(maxLength: 100, nullable: false),
                    Website = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_Stocks", x => x.Symbol);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_AppUserStocks",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    Symbol = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_AppUserStocks", x => new { x.AppUserId, x.Symbol });
                    table.ForeignKey(
                        name: "FK_Stocks_AppUserStocks_Stocks_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Stocks_AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_AppUserStocks_Stocks_Stocks_Symbol",
                        column: x => x.Symbol,
                        principalTable: "Stocks_Stocks",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks_StockPrices",
                columns: table => new
                {
                    Symbol = table.Column<string>(maxLength: 20, nullable: false),
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
                    ChangePercent = table.Column<decimal>(type: "decimal(10,5)", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_StockPrices", x => x.Symbol);
                    table.ForeignKey(
                        name: "FK_Stocks_StockPrices_Stocks_Stocks_Symbol",
                        column: x => x.Symbol,
                        principalTable: "Stocks_Stocks",
                        principalColumn: "Symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AppUserStocks_Symbol",
                table: "Stocks_AppUserStocks",
                column: "Symbol");
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
