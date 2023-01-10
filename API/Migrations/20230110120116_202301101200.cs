using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class _202301101200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PayableAt_PayableAtId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Periodicities_PeriodicityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductPId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_RegFeeTypes_RegFeeTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Cities_CityId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "PayableAt");

            migrationBuilder.DropIndex(
                name: "IX_Stores_CityId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PayableAtId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PeriodicityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DsplSeq",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HaKaDocClientId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsByLevel",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsByZone",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPaidCash",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPctOrAmount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPeriodic",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PayableAtId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PeriodicityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ServiceStartDate",
                table: "Products",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "RegFeeTypeId",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "ProductPId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_RegFeeTypeId",
                table: "Products",
                newName: "IX_Products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductPId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SubProductId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventOpTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventOpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockHistoryActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistoryActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMvts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MvtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromStoreId = table.Column<int>(type: "int", nullable: true),
                    FromEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ToStoreId = table.Column<int>(type: "int", nullable: true),
                    ToEmployeeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    InventOpTypeId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMvts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMvts_AspNetUsers_FromEmployeeId",
                        column: x => x.FromEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMvts_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMvts_AspNetUsers_ToEmployeeId",
                        column: x => x.ToEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMvts_InventOpTypes_InventOpTypeId",
                        column: x => x.InventOpTypeId,
                        principalTable: "InventOpTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMvts_Stores_FromStoreId",
                        column: x => x.FromStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockMvts_Stores_ToStoreId",
                        column: x => x.ToStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Discontinued = table.Column<bool>(type: "bit", nullable: false),
                    UnitInStock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    QuantityPerUnite = table.Column<int>(type: "int", nullable: false),
                    UnitsOnOrder = table.Column<int>(type: "int", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WithSerialNumber = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubProducts_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventOps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventOpTypeId = table.Column<int>(type: "int", nullable: true),
                    OpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromStoreId = table.Column<int>(type: "int", nullable: true),
                    FromEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ToStoreId = table.Column<int>(type: "int", nullable: true),
                    ToEmployeeId = table.Column<int>(type: "int", nullable: true),
                    FormNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: true),
                    SubProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventOps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventOps_AspNetUsers_FromEmployeeId",
                        column: x => x.FromEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_AspNetUsers_ToEmployeeId",
                        column: x => x.ToEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_InventOpTypes_InventOpTypeId",
                        column: x => x.InventOpTypeId,
                        principalTable: "InventOpTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_Stores_FromStoreId",
                        column: x => x.FromStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_Stores_ToStoreId",
                        column: x => x.ToStoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventOps_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubProductFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProductFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubProductFeatures_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubProductSNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    SN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProductSNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InventOpId = table.Column<int>(type: "int", nullable: false),
                    StockHistoryActionId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    OldQty = table.Column<int>(type: "int", nullable: false),
                    NewQty = table.Column<int>(type: "int", nullable: false),
                    Delta = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockHistories_InventOps_InventOpId",
                        column: x => x.InventOpId,
                        principalTable: "InventOps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockHistories_StockHistoryActions_StockHistoryActionId",
                        column: x => x.StockHistoryActionId,
                        principalTable: "StockHistoryActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockHistories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockHistories_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventOpSubProductSNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubProductSNId = table.Column<int>(type: "int", nullable: false),
                    InventOpId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventOpSubProductSNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventOpSubProductSNs_InventOps_InventOpId",
                        column: x => x.InventOpId,
                        principalTable: "InventOps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventOpSubProductSNs_SubProductSNs_SubProductSNId",
                        column: x => x.SubProductSNId,
                        principalTable: "SubProductSNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: true),
                    SubProductSNId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreProducts_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProducts_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreProducts_SubProductSNs_SubProductSNId",
                        column: x => x.SubProductSNId,
                        principalTable: "SubProductSNs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DistrictId",
                table: "Stores",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_SubProductId",
                table: "Photos",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_FromEmployeeId",
                table: "InventOps",
                column: "FromEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_FromStoreId",
                table: "InventOps",
                column: "FromStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_InsertUserId",
                table: "InventOps",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_InventOpTypeId",
                table: "InventOps",
                column: "InventOpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_SubProductId",
                table: "InventOps",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_ToEmployeeId",
                table: "InventOps",
                column: "ToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOps_ToStoreId",
                table: "InventOps",
                column: "ToStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOpSubProductSNs_InventOpId",
                table: "InventOpSubProductSNs",
                column: "InventOpId");

            migrationBuilder.CreateIndex(
                name: "IX_InventOpSubProductSNs_SubProductSNId",
                table: "InventOpSubProductSNs",
                column: "SubProductSNId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories",
                column: "InventOpId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_StockHistoryActionId",
                table: "StockHistories",
                column: "StockHistoryActionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_StoreId",
                table: "StockHistories",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_SubProductId",
                table: "StockHistories",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_UserId",
                table: "StockHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_FromEmployeeId",
                table: "StockMvts",
                column: "FromEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_FromStoreId",
                table: "StockMvts",
                column: "FromStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_InsertUserId",
                table: "StockMvts",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_InventOpTypeId",
                table: "StockMvts",
                column: "InventOpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_ToEmployeeId",
                table: "StockMvts",
                column: "ToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvts_ToStoreId",
                table: "StockMvts",
                column: "ToStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoreId",
                table: "StoreProducts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_SubProductId",
                table: "StoreProducts",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_SubProductSNId",
                table: "StoreProducts",
                column: "SubProductSNId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductFeatures_FeatureId",
                table: "SubProductFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductFeatures_SubProductId",
                table: "SubProductFeatures",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_CategoryId",
                table: "SubProducts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_ProductId",
                table: "SubProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_TypeId",
                table: "SubProducts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_StoreId",
                table: "SubProductSNs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_SubProductId",
                table: "SubProductSNs",
                column: "SubProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_SubProducts_SubProductId",
                table: "Photos",
                column: "SubProductId",
                principalTable: "SubProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AspNetUsers_UserId",
                table: "Stores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_District_DistrictId",
                table: "Stores",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_SubProducts_SubProductId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AspNetUsers_UserId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_District_DistrictId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "InventOpSubProductSNs");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropTable(
                name: "StockMvts");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "SubProductFeatures");

            migrationBuilder.DropTable(
                name: "InventOps");

            migrationBuilder.DropTable(
                name: "StockHistoryActions");

            migrationBuilder.DropTable(
                name: "SubProductSNs");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "InventOpTypes");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DistrictId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_UserId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Photos_SubProductId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Products",
                newName: "ServiceStartDate");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "RegFeeTypeId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "ProductPId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                newName: "IX_Products_RegFeeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_ProductPId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "Stores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Stores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "DsplSeq",
                table: "Products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "HaKaDocClientId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsByLevel",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsByZone",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidCash",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPctOrAmount",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPeriodic",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PayableAtId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodicityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PayableAt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayableAt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayableAt_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CityId",
                table: "Stores",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PayableAtId",
                table: "Products",
                column: "PayableAtId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PeriodicityId",
                table: "Products",
                column: "PeriodicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayableAt_HaKaDocClientId",
                table: "PayableAt",
                column: "HaKaDocClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId",
                principalTable: "HaKaDocClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PayableAt_PayableAtId",
                table: "Products",
                column: "PayableAtId",
                principalTable: "PayableAt",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Periodicities_PeriodicityId",
                table: "Products",
                column: "PeriodicityId",
                principalTable: "Periodicities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductPId",
                table: "Products",
                column: "ProductPId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RegFeeTypes_RegFeeTypeId",
                table: "Products",
                column: "RegFeeTypeId",
                principalTable: "RegFeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Cities_CityId",
                table: "Stores",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
