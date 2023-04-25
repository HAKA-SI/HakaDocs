using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingCustomerIdToStockMvt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "StockMvts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_CustomerId",
                table: "StockMvts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMvts_Customers_CustomerId",
                table: "StockMvts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMvts_Customers_CustomerId",
                table: "StockMvts");

            migrationBuilder.DropIndex(
                name: "IX_StockMvts_CustomerId",
                table: "StockMvts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "StockMvts");
        }
    }
}
