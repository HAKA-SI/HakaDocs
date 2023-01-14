using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addingHakaDocClientIdToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "SubProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_HaKaDocClientId",
                table: "SubProducts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_HaKaDocClientId",
                table: "Stores",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HaKaDocClientId",
                table: "Categories",
                column: "HaKaDocClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_HaKaDocClients_HaKaDocClientId",
                table: "Categories",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_HaKaDocClients_HaKaDocClientId",
                table: "Stores",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubProducts_HaKaDocClients_HaKaDocClientId",
                table: "SubProducts",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_HaKaDocClients_HaKaDocClientId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_HaKaDocClients_HaKaDocClientId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_SubProducts_HaKaDocClients_HaKaDocClientId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_SubProducts_HaKaDocClientId",
                table: "SubProducts");

            migrationBuilder.DropIndex(
                name: "IX_Stores_HaKaDocClientId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_HaKaDocClientId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "SubProducts");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Categories");
        }
    }
}
