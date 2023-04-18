using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingCustumerToInventOP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InventOps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_CustomerId",
                table: "InventOps",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventOps_Customers_CustomerId",
                table: "InventOps",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventOps_Customers_CustomerId",
                table: "InventOps");

            migrationBuilder.DropIndex(
                name: "IX_InventOps_CustomerId",
                table: "InventOps");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InventOps");
        }
    }
}
