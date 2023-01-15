using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingProductGroupToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductGroupId",
                table: "Categories",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductGroups_ProductGroupId",
                table: "Categories",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductGroups_ProductGroupId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductGroupId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "Categories");
        }
    }
}
