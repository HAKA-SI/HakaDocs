using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class removeStockAlertQutity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stockAlertQuantity",
                table: "SubProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stockAlertQuantity",
                table: "SubProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
