using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class InvoiceOrderline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_OrderLines_OrderLineId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_OrderLineId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OrderLineId",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "InvoiceOrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceOrderLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceOrderLines_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderLines_InvoiceId",
                table: "InvoiceOrderLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderLines_OrderLineId",
                table: "InvoiceOrderLines",
                column: "OrderLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceOrderLines");

            migrationBuilder.AddColumn<int>(
                name: "OrderLineId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderLineId",
                table: "Invoices",
                column: "OrderLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_OrderLines_OrderLineId",
                table: "Invoices",
                column: "OrderLineId",
                principalTable: "OrderLines",
                principalColumn: "Id");
        }
    }
}
