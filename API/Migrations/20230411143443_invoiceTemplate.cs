using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class invoiceTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InvoiceTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceTemplates_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_HaKaDocClientId",
                table: "InvoiceTemplates",
                column: "HaKaDocClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceTemplates");

            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "Orders");
        }
    }
}
