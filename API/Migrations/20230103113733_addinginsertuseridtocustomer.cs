using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addinginsertuseridtocustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InsertUserId",
                table: "Customers",
                column: "InsertUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_InsertUserId",
                table: "Customers",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_InsertUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_InsertUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Customers");
        }
    }
}
