using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BevCapital.Stocks.Data.Migrations.EventStore
{
    public partial class EventStore_InitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks_EventsStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateId = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks_EventsStore", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EventsStore_AggregateId_Version",
                table: "Stocks_EventsStore",
                columns: new[] { "AggregateId", "Version" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks_EventsStore");
        }
    }
}
