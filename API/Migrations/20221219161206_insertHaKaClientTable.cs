using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class insertHaKaClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_User_ForUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_User_InsertUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_User_UpdateUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_ChildId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_FatherId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_MotherId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ChildId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FatherId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MotherId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FatherId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MotherId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "ProductTypes",
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
                table: "Periodicities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "PayableAt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Fees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "EmailTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Docs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertUserId",
                table: "Docs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "DeadLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Cheques",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "CashDesks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HaKaDocClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaKaDocClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HaKaDocClients_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_HaKaDocClientId",
                table: "User",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_HaKaDocClientId",
                table: "Receipts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_HaKaDocClientId",
                table: "ProductTypes",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Periodicities_HaKaDocClientId",
                table: "Periodicities",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableAt_HaKaDocClientId",
                table: "PayableAt",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_HaKaDocClientId",
                table: "Invoices",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_HaKaDocClientId",
                table: "Fees",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_HaKaDocClientId",
                table: "EmailTemplates",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_HaKaDocClientId",
                table: "Docs",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_InsertUserId",
                table: "Docs",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_HaKaDocClientId",
                table: "Discounts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadLines_HaKaDocClientId",
                table: "DeadLines",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_HaKaDocClientId",
                table: "Cheques",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDesks_HaKaDocClientId",
                table: "CashDesks",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HaKaDocClientId",
                table: "Customer",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_HaKaDocClients_CityId",
                table: "HaKaDocClients",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashDesks_HaKaDocClients_HaKaDocClientId",
                table: "CashDesks",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_HaKaDocClients_HaKaDocClientId",
                table: "Cheques",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeadLines_HaKaDocClients_HaKaDocClientId",
                table: "DeadLines",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_HaKaDocClients_HaKaDocClientId",
                table: "Discounts",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_AspNetUsers_InsertUserId",
                table: "Docs",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_HaKaDocClients_HaKaDocClientId",
                table: "Docs",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_HaKaDocClients_HaKaDocClientId",
                table: "EmailTemplates",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_HaKaDocClients_HaKaDocClientId",
                table: "Fees",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_AspNetUsers_ForUserId",
                table: "FinOps",
                column: "ForUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_AspNetUsers_InsertUserId",
                table: "FinOps",
                column: "InsertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_AspNetUsers_UpdateUserId",
                table: "FinOps",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_HaKaDocClients_HaKaDocClientId",
                table: "Invoices",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayableAt_HaKaDocClients_HaKaDocClientId",
                table: "PayableAt",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Periodicities_HaKaDocClients_HaKaDocClientId",
                table: "Periodicities",
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
                name: "FK_ProductTypes_HaKaDocClients_HaKaDocClientId",
                table: "ProductTypes",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_HaKaDocClients_HaKaDocClientId",
                table: "Receipts",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_HaKaDocClients_HaKaDocClientId",
                table: "User",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashDesks_HaKaDocClients_HaKaDocClientId",
                table: "CashDesks");

            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_HaKaDocClients_HaKaDocClientId",
                table: "Cheques");

            migrationBuilder.DropForeignKey(
                name: "FK_DeadLines_HaKaDocClients_HaKaDocClientId",
                table: "DeadLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_HaKaDocClients_HaKaDocClientId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Docs_AspNetUsers_InsertUserId",
                table: "Docs");

            migrationBuilder.DropForeignKey(
                name: "FK_Docs_HaKaDocClients_HaKaDocClientId",
                table: "Docs");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_HaKaDocClients_HaKaDocClientId",
                table: "EmailTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Fees_HaKaDocClients_HaKaDocClientId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_AspNetUsers_ForUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_AspNetUsers_InsertUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_FinOps_AspNetUsers_UpdateUserId",
                table: "FinOps");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_HaKaDocClients_HaKaDocClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customer_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PayableAt_HaKaDocClients_HaKaDocClientId",
                table: "PayableAt");

            migrationBuilder.DropForeignKey(
                name: "FK_Periodicities_HaKaDocClients_HaKaDocClientId",
                table: "Periodicities");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTypes_HaKaDocClients_HaKaDocClientId",
                table: "ProductTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_HaKaDocClients_HaKaDocClientId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_User_HaKaDocClients_HaKaDocClientId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "HaKaDocClients");

            migrationBuilder.DropIndex(
                name: "IX_User_HaKaDocClientId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_HaKaDocClientId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_HaKaDocClientId",
                table: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Periodicities_HaKaDocClientId",
                table: "Periodicities");

            migrationBuilder.DropIndex(
                name: "IX_PayableAt_HaKaDocClientId",
                table: "PayableAt");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_HaKaDocClientId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Fees_HaKaDocClientId",
                table: "Fees");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_HaKaDocClientId",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Docs_HaKaDocClientId",
                table: "Docs");

            migrationBuilder.DropIndex(
                name: "IX_Docs_InsertUserId",
                table: "Docs");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_HaKaDocClientId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_DeadLines_HaKaDocClientId",
                table: "DeadLines");

            migrationBuilder.DropIndex(
                name: "IX_Cheques_HaKaDocClientId",
                table: "Cheques");

            migrationBuilder.DropIndex(
                name: "IX_CashDesks_HaKaDocClientId",
                table: "CashDesks");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Periodicities");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "PayableAt");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "DeadLines");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Cheques");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "CashDesks");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FatherId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotherId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ChildId",
                table: "Orders",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FatherId",
                table: "Orders",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MotherId",
                table: "Orders",
                column: "MotherId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_User_ForUserId",
                table: "FinOps",
                column: "ForUserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_User_InsertUserId",
                table: "FinOps",
                column: "InsertUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinOps_User_UpdateUserId",
                table: "FinOps",
                column: "UpdateUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_ChildId",
                table: "Orders",
                column: "ChildId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_FatherId",
                table: "Orders",
                column: "FatherId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_MotherId",
                table: "Orders",
                column: "MotherId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
