using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingInseruserToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "SubProductSNs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "SubProductSNs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "SubProductSNs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "SubProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "SubProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_HaKaDocClientId",
                table: "SubProductSNs",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_InsertUserId",
                table: "SubProductSNs",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_UpdateUserId",
                table: "SubProductSNs",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_InsertUserId",
                table: "SubProducts",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_UpdateUserId",
                table: "SubProducts",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InsertUserId",
                table: "Products",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdateUserId",
                table: "Products",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_InsertUserId",
                table: "Categories",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdateUserId",
                table: "Categories",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_InsertUserId",
                table: "Categories",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UpdateUserId",
                table: "Categories",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_InsertUserId",
                table: "Products",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UpdateUserId",
                table: "Products",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_AspNetUsers_InsertUserId",
                table: "SubProducts",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_AspNetUsers_UpdateUserId",
                table: "SubProducts",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProductSNs_AspNetUsers_InsertUserId",
                table: "SubProductSNs",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProductSNs_AspNetUsers_UpdateUserId",
                table: "SubProductSNs",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubProductSNs_HaKaDocClients_HaKaDocClientId",
                table: "SubProductSNs",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_InsertUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UpdateUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_InsertUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UpdateUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_AspNetUsers_InsertUserId",
                table: "SubProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_AspNetUsers_UpdateUserId",
                table: "SubProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProductSNs_AspNetUsers_InsertUserId",
                table: "SubProductSNs");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProductSNs_AspNetUsers_UpdateUserId",
                table: "SubProductSNs");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProductSNs_HaKaDocClients_HaKaDocClientId",
                table: "SubProductSNs");

            migrationBuilder.DropIndex(
                name: "IX_SubProductSNs_HaKaDocClientId",
                table: "SubProductSNs");

            migrationBuilder.DropIndex(
                name: "IX_SubProductSNs_InsertUserId",
                table: "SubProductSNs");

            migrationBuilder.DropIndex(
                name: "IX_SubProductSNs_UpdateUserId",
                table: "SubProductSNs");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_InsertUserId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_UpdateUserId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_InsertUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdateUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_InsertUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdateUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "SubProductSNs");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "SubProductSNs");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "SubProductSNs");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Categories");
        }
    }
}
