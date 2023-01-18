using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addinginventOpStockMvts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockMvtInventOps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockMvtId = table.Column<int>(type: "int", nullable: false),
                    InventOpId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMvtInventOps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMvtInventOps_InventOps_InventOpId",
                        column: x => x.InventOpId,
                        principalTable: "InventOps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMvtInventOps_StockMvts_StockMvtId",
                        column: x => x.StockMvtId,
                        principalTable: "StockMvts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMvtInventOps_InventOpId",
                table: "StockMvtInventOps",
                column: "InventOpId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvtInventOps_StockMvtId",
                table: "StockMvtInventOps",
                column: "StockMvtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMvtInventOps");
        }
    }
}
