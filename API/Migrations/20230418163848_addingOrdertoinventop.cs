using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingOrdertoinventop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "InventOps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_OrderId",
                table: "InventOps",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventOps_Orders_OrderId",
                table: "InventOps",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventOps_Orders_OrderId",
                table: "InventOps");

            migrationBuilder.DropIndex(
                name: "IX_InventOps_OrderId",
                table: "InventOps");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "InventOps");
        }
    }
}
