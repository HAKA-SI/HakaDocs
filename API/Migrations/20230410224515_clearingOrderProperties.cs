using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class clearingOrderProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Validity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isNextReg",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "isReg",
                table: "Orders",
                newName: "FullyPaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullyPaid",
                table: "Orders",
                newName: "isReg");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Validity",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isNextReg",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
