using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class initialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactCell = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountTypePId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountTypes_DiscountTypes_DiscountTypePId",
                        column: x => x.DiscountTypePId,
                        principalTable: "DiscountTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocTypes",
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
                    table.PrimaryKey("PK_DocTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTypes", x => x.Id);
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
                name: "FinOpTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinOpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Name);
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
                name: "MaritalStatuses",
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
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DsplSeq = table.Column<byte>(type: "tinyint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
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
                name: "RegFeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegFeeTypePId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegFeeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegFeeTypes_RegFeeTypes_RegFeeTypePId",
                        column: x => x.RegFeeTypePId,
                        principalTable: "RegFeeTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SmsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsTypes", x => x.Id);
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
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "Groups",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Capabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    AccessType = table.Column<byte>(type: "tinyint", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Capabilities_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    CityId = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaKaDocClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HaKaDocClients_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Address_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Address_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: true),
                    ValidationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodeValidated = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CashDesks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDesks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashDesks_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cheques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: true),
                    ChequeNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cheques_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    CodeLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCodes_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeadLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeadLines_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCategoryId = table.Column<int>(type: "int", nullable: false),
                    Internal = table.Column<bool>(type: "bit", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailCategories_EmailCategoryId",
                        column: x => x.EmailCategoryId,
                        principalTable: "EmailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<double>(type: "float", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fees_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Periodicities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbrev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbDays = table.Column<byte>(type: "tinyint", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodicities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periodicities_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypePId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypes_ProductTypes_ProductTypePId",
                        column: x => x.ProductTypePId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    ProductGroupId = table.Column<int>(type: "int", nullable: true),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    IdNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<byte>(type: "tinyint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    BirthCountryId = table.Column<int>(type: "int", nullable: true),
                    BirthCityId = table.Column<int>(type: "int", nullable: true),
                    BirthDistrictId = table.Column<int>(type: "int", nullable: true),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: true),
                    TempData = table.Column<byte>(type: "tinyint", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    ToBeValidatedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountDataValidated = table.Column<bool>(type: "bit", nullable: false),
                    PostalBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalIDNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    Cni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iddoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Cities_BirthCityId",
                        column: x => x.BirthCityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Countries_BirthCountryId",
                        column: x => x.BirthCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Districts_BirthDistrictId",
                        column: x => x.BirthDistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocTypeId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Docs_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Docs_DocTypes_DocTypeId",
                        column: x => x.DocTypeId,
                        principalTable: "DocTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Docs_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailTypeId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BCCAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeToSend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusFlag = table.Column<byte>(type: "tinyint", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_EmailTypes_EmailTypeId",
                        column: x => x.EmailTypeId,
                        principalTable: "EmailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    RecipientUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MessageSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecipientDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stores_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stores_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    OrderLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: true),
                    BillingAddressId = table.Column<int>(type: "int", nullable: true),
                    TotalHT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountHT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TVAAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountTTC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    Overdue = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    FullyPaid = table.Column<bool>(type: "bit", nullable: false),
                    Delivered = table.Column<bool>(type: "bit", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Address_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "StoreUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreUsers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountTypeId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    DiscountProductTypeId = table.Column<int>(type: "int", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQtyMin = table.Column<int>(type: "int", nullable: false),
                    AmountMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discounts_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discounts_ProductTypes_DiscountProductTypeId",
                        column: x => x.DiscountProductTypeId,
                        principalTable: "ProductTypes",
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
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    VatExempted = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProducts_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProducts_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProducts_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProducts_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<bool>(type: "bit", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    Overdue = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Delta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
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
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderLineLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    ProductFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalHT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountHT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TVAAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountTTC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Validity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: true),
                    Validated = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false),
                    OverDue = table.Column<bool>(type: "bit", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_AspNetUsers_ChildId",
                        column: x => x.ChildId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photos_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Photos_SubProducts_SubProductId",
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProductFeatures_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    HaKaDocClientId = table.Column<int>(type: "int", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    CanBeDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProductSNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubProductSNs_HaKaDocClients_HaKaDocClientId",
                        column: x => x.HaKaDocClientId,
                        principalTable: "HaKaDocClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubProductSNs_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinOps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinOpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinOpTypeId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    ChequeId = table.Column<int>(type: "int", nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromUserId = table.Column<int>(type: "int", nullable: true),
                    FromOtherUserId = table.Column<int>(type: "int", nullable: true),
                    ForUserId = table.Column<int>(type: "int", nullable: true),
                    FromBankId = table.Column<int>(type: "int", nullable: true),
                    FromCashDeskId = table.Column<int>(type: "int", nullable: true),
                    FromBankAccountId = table.Column<int>(type: "int", nullable: true),
                    ToCashDeskId = table.Column<int>(type: "int", nullable: true),
                    ToBankAccountId = table.Column<int>(type: "int", nullable: true),
                    DocRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Received = table.Column<bool>(type: "bit", nullable: false),
                    DepositedToBank = table.Column<bool>(type: "bit", nullable: false),
                    Cashed = table.Column<bool>(type: "bit", nullable: false),
                    Rejected = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinOps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinOps_AspNetUsers_ForUserId",
                        column: x => x.ForUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_AspNetUsers_FromOtherUserId",
                        column: x => x.FromOtherUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinOps_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinOps_BankAccounts_FromBankAccountId",
                        column: x => x.FromBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_BankAccounts_ToBankAccountId",
                        column: x => x.ToBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_Banks_FromBankId",
                        column: x => x.FromBankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_CashDesks_FromCashDeskId",
                        column: x => x.FromCashDeskId,
                        principalTable: "CashDesks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_CashDesks_ToCashDeskId",
                        column: x => x.ToCashDeskId,
                        principalTable: "CashDesks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_Cheques_ChequeId",
                        column: x => x.ChequeId,
                        principalTable: "Cheques",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_FinOpTypes_FinOpTypeId",
                        column: x => x.FinOpTypeId,
                        principalTable: "FinOpTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinOps_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_InventOps_InventOpId",
                        column: x => x.InventOpId,
                        principalTable: "InventOps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_StockHistoryActions_StockHistoryActionId",
                        column: x => x.StockHistoryActionId,
                        principalTable: "StockHistoryActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_SubProducts_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMvtInventOps_StockMvts_StockMvtId",
                        column: x => x.StockMvtId,
                        principalTable: "StockMvts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceOrderLines_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineDeadlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seq = table.Column<byte>(type: "tinyint", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineDeadlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineDeadlines_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineDeadlines_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineDeadlines_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineReceipts_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineReceipts_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineRegFees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    RegFeeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineRegFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineRegFees_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineRegFees_Products_RegFeeId",
                        column: x => x.RegFeeId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventOpSubProductSNs_SubProductSNs_SubProductSNId",
                        column: x => x.SubProductSNId,
                        principalTable: "SubProductSNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineSubProductSNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubProductSNId = table.Column<int>(type: "int", nullable: false),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    DiscountAmout = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineSubProductSNs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineSubProductSNs_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineSubProductSNs_SubProductSNs_SubProductSNId",
                        column: x => x.SubProductSNId,
                        principalTable: "SubProductSNs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "OrderLineHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    FinOpId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Delta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cashed = table.Column<bool>(type: "bit", nullable: false),
                    Rejected = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineHistories_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineHistories_AspNetUsers_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineHistories_FinOps_FinOpId",
                        column: x => x.FinOpId,
                        principalTable: "FinOps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderLineHistories_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_DistrictId",
                table: "Address",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_HaKaDocClientId",
                table: "AspNetRoles",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HaKaDocClientId",
                table: "AspNetUsers",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Capabilities_MenuItemId",
                table: "Capabilities",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDesks_HaKaDocClientId",
                table: "CashDesks",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_HaKaDocClientId",
                table: "Categories",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_InsertUserId",
                table: "Categories",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductGroupId",
                table: "Categories",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdateUserId",
                table: "Categories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_BankId",
                table: "Cheques",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_HaKaDocClientId",
                table: "Cheques",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_GroupName",
                table: "Connections",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCodes_HaKaDocClientId",
                table: "CustomerCodes",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BirthCityId",
                table: "Customers",
                column: "BirthCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BirthCountryId",
                table: "Customers",
                column: "BirthCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BirthDistrictId",
                table: "Customers",
                column: "BirthDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityId",
                table: "Customers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CountryId",
                table: "Customers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DistrictId",
                table: "Customers",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_HaKaDocClientId",
                table: "Customers",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InsertUserId",
                table: "Customers",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MaritalStatusId",
                table: "Customers",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DeadLines_HaKaDocClientId",
                table: "DeadLines",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountProductTypeId",
                table: "Discounts",
                column: "DiscountProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_HaKaDocClientId",
                table: "Discounts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductId",
                table: "Discounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountTypes_DiscountTypePId",
                table: "DiscountTypes",
                column: "DiscountTypePId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_DocTypeId",
                table: "Docs",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_HaKaDocClientId",
                table: "Docs",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Docs_InsertUserId",
                table: "Docs",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_EmailTypeId",
                table: "Emails",
                column: "EmailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_RecipientId",
                table: "Emails",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_SenderId",
                table: "Emails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailCategoryId",
                table: "EmailTemplates",
                column: "EmailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_HaKaDocClientId",
                table: "EmailTemplates",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_HaKaDocClientId",
                table: "Fees",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_ChequeId",
                table: "FinOps",
                column: "ChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FinOpTypeId",
                table: "FinOps",
                column: "FinOpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_ForUserId",
                table: "FinOps",
                column: "ForUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FromBankAccountId",
                table: "FinOps",
                column: "FromBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FromBankId",
                table: "FinOps",
                column: "FromBankId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FromCashDeskId",
                table: "FinOps",
                column: "FromCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FromOtherUserId",
                table: "FinOps",
                column: "FromOtherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_FromUserId",
                table: "FinOps",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_InsertUserId",
                table: "FinOps",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_InvoiceId",
                table: "FinOps",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_OrderId",
                table: "FinOps",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_PaymentTypeId",
                table: "FinOps",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_ToBankAccountId",
                table: "FinOps",
                column: "ToBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_ToCashDeskId",
                table: "FinOps",
                column: "ToCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_FinOps_UpdateUserId",
                table: "FinOps",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HaKaDocClients_CityId",
                table: "HaKaDocClients",
                column: "CityId");

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
                name: "IX_InvoiceOrderLines_InvoiceId",
                table: "InvoiceOrderLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceOrderLines_OrderLineId",
                table: "InvoiceOrderLines",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_HaKaDocClientId",
                table: "Invoices",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplates_HaKaDocClientId",
                table: "InvoiceTemplates",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_UserTypeId",
                table: "Menus",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderId",
                table: "OrderHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_UserId",
                table: "OrderHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineDeadlines_InsertUserId",
                table: "OrderLineDeadlines",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineDeadlines_OrderLineId",
                table: "OrderLineDeadlines",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineDeadlines_UpdateUserId",
                table: "OrderLineDeadlines",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineHistories_FinOpId",
                table: "OrderLineHistories",
                column: "FinOpId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineHistories_InsertUserId",
                table: "OrderLineHistories",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineHistories_OrderLineId",
                table: "OrderLineHistories",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineHistories_UpdateUserId",
                table: "OrderLineHistories",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineHistories_UserId",
                table: "OrderLineHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineReceipts_OrderLineId",
                table: "OrderLineReceipts",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineReceipts_ReceiptId",
                table: "OrderLineReceipts",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineRegFees_OrderLineId",
                table: "OrderLineRegFees",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineRegFees_RegFeeId",
                table: "OrderLineRegFees",
                column: "RegFeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ChildId",
                table: "OrderLines",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_SubProductId",
                table: "OrderLines",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineSubProductSNs_OrderLineId",
                table: "OrderLineSubProductSNs",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineSubProductSNs_SubProductSNId",
                table: "OrderLineSubProductSNs",
                column: "SubProductSNId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Periodicities_HaKaDocClientId",
                table: "Periodicities",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CustomerId",
                table: "Photos",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_SubProductId",
                table: "Photos",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_HaKaDocClientId",
                table: "Products",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InsertUserId",
                table: "Products",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdateUserId",
                table: "Products",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_HaKaDocClientId",
                table: "ProductTypes",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductTypePId",
                table: "ProductTypes",
                column: "ProductTypePId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_HaKaDocClientId",
                table: "Receipts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RegFeeTypes_RegFeeTypePId",
                table: "RegFeeTypes",
                column: "RegFeeTypePId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_InventOpId",
                table: "StockHistories",
                column: "InventOpId",
                unique: true);

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
                name: "IX_StockMvtInventOps_InventOpId",
                table: "StockMvtInventOps",
                column: "InventOpId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMvtInventOps_StockMvtId",
                table: "StockMvtInventOps",
                column: "StockMvtId");

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
                name: "IX_Stores_DistrictId",
                table: "Stores",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_HaKaDocClientId",
                table: "Stores",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_StoreId",
                table: "StoreUsers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUsers_UserId",
                table: "StoreUsers",
                column: "UserId");

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
                name: "IX_SubProducts_HaKaDocClientId",
                table: "SubProducts",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_InsertUserId",
                table: "SubProducts",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_ProductId",
                table: "SubProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_TypeId",
                table: "SubProducts",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_UpdateUserId",
                table: "SubProducts",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_HaKaDocClientId",
                table: "SubProductSNs",
                column: "HaKaDocClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_InsertUserId",
                table: "SubProductSNs",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_StoreId",
                table: "SubProductSNs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_SubProductId",
                table: "SubProductSNs",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProductSNs_UpdateUserId",
                table: "SubProductSNs",
                column: "UpdateUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Capabilities");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "CustomerCodes");

            migrationBuilder.DropTable(
                name: "DeadLines");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "InventOpSubProductSNs");

            migrationBuilder.DropTable(
                name: "InvoiceOrderLines");

            migrationBuilder.DropTable(
                name: "InvoiceTemplates");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropTable(
                name: "OrderLineDeadlines");

            migrationBuilder.DropTable(
                name: "OrderLineHistories");

            migrationBuilder.DropTable(
                name: "OrderLineReceipts");

            migrationBuilder.DropTable(
                name: "OrderLineRegFees");

            migrationBuilder.DropTable(
                name: "OrderLineSubProductSNs");

            migrationBuilder.DropTable(
                name: "Periodicities");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "RegFeeTypes");

            migrationBuilder.DropTable(
                name: "SmsTypes");

            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropTable(
                name: "StockMvtInventOps");

            migrationBuilder.DropTable(
                name: "StoreProducts");

            migrationBuilder.DropTable(
                name: "StoreUsers");

            migrationBuilder.DropTable(
                name: "SubProductFeatures");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "DocTypes");

            migrationBuilder.DropTable(
                name: "EmailTypes");

            migrationBuilder.DropTable(
                name: "EmailCategories");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "FinOps");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "StockHistoryActions");

            migrationBuilder.DropTable(
                name: "InventOps");

            migrationBuilder.DropTable(
                name: "StockMvts");

            migrationBuilder.DropTable(
                name: "SubProductSNs");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CashDesks");

            migrationBuilder.DropTable(
                name: "Cheques");

            migrationBuilder.DropTable(
                name: "FinOpTypes");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "InventOpTypes");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "HaKaDocClients");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
