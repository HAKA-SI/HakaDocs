using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AllowNullStoreIdInSubPorductSn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProductSNs_Stores_StoreId",
                table: "SubProductSNs");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "SubProductSNs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProductSNs_Stores_StoreId",
                table: "SubProductSNs",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubProductSNs_Stores_StoreId",
                table: "SubProductSNs");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "SubProductSNs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProductSNs_Stores_StoreId",
                table: "SubProductSNs",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
