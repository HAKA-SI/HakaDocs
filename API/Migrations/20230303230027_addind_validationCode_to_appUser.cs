using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addind_validationCode_to_appUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories");

            migrationBuilder.AddColumn<string>(
                name: "ValidationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories",
                column: "InventOpId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories");

            migrationBuilder.DropColumn(
                name: "ValidationCode",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories",
                column: "InventOpId");
        }
    }
}
