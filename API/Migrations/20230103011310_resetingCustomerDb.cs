using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class resetingCustomerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Cities_BirthCityId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Cities_CityId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Countries_BirthCountryId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Countries_CountryId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_District_BirthDistrictId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_District_DistrictId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_HaKaDocClients_HaKaDocClientId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_MaritalStatuses_MaritalStatusId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Customer_CustomerId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_MaritalStatusId",
                table: "Customers",
                newName: "IX_Customers_MaritalStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_HaKaDocClientId",
                table: "Customers",
                newName: "IX_Customers_HaKaDocClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_DistrictId",
                table: "Customers",
                newName: "IX_Customers_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CountryId",
                table: "Customers",
                newName: "IX_Customers_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CityId",
                table: "Customers",
                newName: "IX_Customers_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BirthDistrictId",
                table: "Customers",
                newName: "IX_Customers_BirthDistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BirthCountryId",
                table: "Customers",
                newName: "IX_Customers_BirthCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BirthCityId",
                table: "Customers",
                newName: "IX_Customers_BirthCityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Cities_BirthCityId",
                table: "Customers",
                column: "BirthCityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Countries_BirthCountryId",
                table: "Customers",
                column: "BirthCountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Countries_CountryId",
                table: "Customers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_District_BirthDistrictId",
                table: "Customers",
                column: "BirthDistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_District_DistrictId",
                table: "Customers",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_HaKaDocClients_HaKaDocClientId",
                table: "Customers",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_MaritalStatuses_MaritalStatusId",
                table: "Customers",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Customers_CustomerId",
                table: "Photos",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Cities_BirthCityId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Cities_CityId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Countries_BirthCountryId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Countries_CountryId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_District_BirthDistrictId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_District_DistrictId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_HaKaDocClients_HaKaDocClientId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_MaritalStatuses_MaritalStatusId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Customers_CustomerId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_MaritalStatusId",
                table: "Customer",
                newName: "IX_Customer_MaritalStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_HaKaDocClientId",
                table: "Customer",
                newName: "IX_Customer_HaKaDocClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_DistrictId",
                table: "Customer",
                newName: "IX_Customer_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CountryId",
                table: "Customer",
                newName: "IX_Customer_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CityId",
                table: "Customer",
                newName: "IX_Customer_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BirthDistrictId",
                table: "Customer",
                newName: "IX_Customer_BirthDistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BirthCountryId",
                table: "Customer",
                newName: "IX_Customer_BirthCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BirthCityId",
                table: "Customer",
                newName: "IX_Customer_BirthCityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Cities_BirthCityId",
                table: "Customer",
                column: "BirthCityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Cities_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Countries_BirthCountryId",
                table: "Customer",
                column: "BirthCountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Countries_CountryId",
                table: "Customer",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_District_BirthDistrictId",
                table: "Customer",
                column: "BirthDistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_District_DistrictId",
                table: "Customer",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_HaKaDocClients_HaKaDocClientId",
                table: "Customer",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_MaritalStatuses_MaritalStatusId",
                table: "Customer",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Customer_CustomerId",
                table: "Photos",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
