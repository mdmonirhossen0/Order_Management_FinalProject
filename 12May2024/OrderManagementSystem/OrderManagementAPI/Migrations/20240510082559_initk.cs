using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class initk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Procurements",
                columns: table => new
                {
                    ProcurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procurements", x => x.ProcurementId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "SaleHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "date", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBill = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    RequisitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.RequisitionId);
                    table.ForeignKey(
                        name: "FK_Requisitions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementDetails",
                columns: table => new
                {
                    ProcurementDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementId = table.Column<int>(type: "int", nullable: false),
                    ProcurementQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementDetails", x => x.ProcurementDetailsId);
                    table.ForeignKey(
                        name: "FK_ProcurementDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcurementDetails_Procurements_ProcurementId",
                        column: x => x.ProcurementId,
                        principalTable: "Procurements",
                        principalColumn: "ProcurementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyMenus",
                columns: table => new
                {
                    DailyMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyMenuDate = table.Column<DateTime>(type: "date", nullable: false),
                    DemandQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CookedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServingQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenus", x => x.DailyMenuId);
                    table.ForeignKey(
                        name: "FK_DailyMenus_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeItems",
                columns: table => new
                {
                    RecipeItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItems", x => x.RecipeItemId);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleHeaderId = table.Column<int>(type: "int", nullable: false),
                    DailyMenuId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleDetails_DailyMenus_DailyMenuId",
                        column: x => x.DailyMenuId,
                        principalTable: "DailyMenus",
                        principalColumn: "DailyMenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetails_SaleHeader_SaleHeaderId",
                        column: x => x.SaleHeaderId,
                        principalTable: "SaleHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Name", "Type", "Unit" },
                values: new object[,]
                {
                    { 1, "Rice", "Non-perishable", "KG" },
                    { 2, "Salt", "Perishable", "KG" },
                    { 3, "Soyabean Oil", "Perishable", "Litter" },
                    { 4, "Mustard Oil", "Perishable", "Litter" },
                    { 5, "Bashmoti Rice", "Non-perishable", "KG" },
                    { 6, "Mutton", "Perishable", "KG" },
                    { 7, "Beef", "Perishable", "KG" },
                    { 8, "Chili", "Perishable", "KG" },
                    { 9, "Turmeric Powder", "Non-perishable", "KG" },
                    { 10, "Ginger", "Perishable", "KG" },
                    { 11, "Onion", "Perishable", "KG" },
                    { 12, "Garlic", "Perishable", "KG" },
                    { 13, "Sugar", "Non-perishable", "KG" },
                    { 14, "Yogurt", "Perishable", "Litter" },
                    { 15, "Milk", "Perishable", "Litter" },
                    { 16, "Ketchup", "Perishable", "Litter" },
                    { 17, "Raisin", "Non-perishable", "KG" },
                    { 18, "Bay Leaves", "Non-perishable", "KG" },
                    { 19, "Green Candamon", "Non-perishable", "KG" },
                    { 20, "Cloves", "Non-perishable", "KG" },
                    { 21, "Nut", "Non-perishable", "KG" },
                    { 22, "Lentils", "Non-perishable", "KG" },
                    { 23, "Capsicum", "Perishable", "KG" },
                    { 24, "Peas", "Non-perishable", "KG" },
                    { 25, "Butter", "Perishable", "KG" },
                    { 26, "Egg", "Perishable", "Piece" },
                    { 27, "Chicken", "Perishable", "KG" },
                    { 28, "Corn flour", "Non-perishable", "KG" },
                    { 29, "Tomato", "Perishable", "KG" },
                    { 30, "Chili Powder", "Non-perishable", "KG" },
                    { 31, "Garam Masala Powder", "Non-perishable", "KG" },
                    { 32, "Cotton Fish", "Perishable", "KG" },
                    { 33, "Shrimp Fish", "Perishable", "KG" },
                    { 34, "Katla Fish", "Perishable", "KG" },
                    { 35, "Hilsa Fish", "Perishable", "KG" },
                    { 36, "Mustard", "Non-perishable", "KG" },
                    { 37, "Coconut", "Perishable", "Piece" },
                    { 38, "Ghee", "Perishable", "Litter" },
                    { 39, "Cheery", "Perishable", "KG" },
                    { 40, "Zafran", "Non-perishable", "KG" },
                    { 41, "Cinnamon", "Non-perishable", "KG" },
                    { 42, "Veggies", "Perishable", "KG" },
                    { 43, "Oster Sauce", "Non-perishable", "KG" },
                    { 44, "Soy sauce", "Non-perishable", "KG" },
                    { 45, "Sesame oil", "Non-perishable", "KG" },
                    { 46, "Cumin Powder", "Non-perishable", "KG" },
                    { 47, "Coriander Powder", "Non-perishable", "KG" },
                    { 48, "Rose-water", "Non-perishable", "KG" },
                    { 49, "Lime Juice", "Non-perishable", "KG" },
                    { 50, "Cinnamon", "Non-Perishable", "KG" },
                    { 51, "Lemon Juice", "Perishable", "Litter" },
                    { 52, "Mint Leaves", "Perishable", "KG" },
                    { 53, "Cumin", "Non-Perishable", "KG" },
                    { 54, "Korma Paste", "Perishable", "Litter" },
                    { 55, "Almond", "Non-Perishable", "KG" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "RecipeName" },
                values: new object[,]
                {
                    { 1, "Plain Rice" },
                    { 2, "Kachchi" },
                    { 3, "Polao" },
                    { 4, "Biriyani" },
                    { 5, "Khicuri" },
                    { 6, "Fried Rice" },
                    { 7, "Beef Rezala" },
                    { 8, "Chicken Roast" },
                    { 9, "Spicy Dry Chicken" },
                    { 10, "Fried Chicken" },
                    { 11, "Mutton Curry" },
                    { 12, "Korma" },
                    { 13, "Kalia" },
                    { 14, "Shorshe Ilish" },
                    { 15, "Chingri Malai Curry" },
                    { 16, "Kala Bhuna" },
                    { 17, "Fish Curry" },
                    { 18, "Haleem" },
                    { 19, "Jorda" },
                    { 20, "Firni" }
                });

            migrationBuilder.InsertData(
                table: "RecipeItems",
                columns: new[] { "RecipeItemId", "ItemId", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, 1, 1m, 1 },
                    { 2, 5, 1m, 2 },
                    { 3, 3, 1m, 2 },
                    { 4, 6, 1m, 2 },
                    { 5, 9, 1m, 2 },
                    { 6, 10, 1m, 2 },
                    { 7, 11, 1m, 2 },
                    { 8, 12, 1m, 2 },
                    { 9, 13, 1m, 2 },
                    { 10, 14, 1m, 2 },
                    { 11, 16, 1m, 2 },
                    { 12, 17, 1m, 2 },
                    { 13, 18, 1m, 2 },
                    { 14, 19, 1m, 2 },
                    { 15, 20, 1m, 2 },
                    { 16, 21, 1m, 2 },
                    { 17, 30, 1m, 2 },
                    { 18, 31, 1m, 2 },
                    { 19, 38, 1m, 2 },
                    { 20, 40, 1m, 2 },
                    { 21, 1, 1m, 3 },
                    { 22, 2, 1m, 3 },
                    { 23, 3, 1m, 3 },
                    { 24, 8, 1m, 3 },
                    { 25, 15, 1m, 3 },
                    { 26, 17, 1m, 3 },
                    { 27, 18, 1m, 3 },
                    { 28, 19, 1m, 3 },
                    { 29, 41, 1m, 3 },
                    { 30, 1, 1m, 4 },
                    { 31, 2, 1m, 4 },
                    { 32, 3, 1m, 4 },
                    { 33, 7, 1m, 4 },
                    { 34, 8, 1m, 4 },
                    { 35, 9, 1m, 4 },
                    { 36, 10, 1m, 4 },
                    { 37, 11, 1m, 4 },
                    { 38, 12, 1m, 4 },
                    { 39, 14, 1m, 4 },
                    { 40, 15, 1m, 4 },
                    { 41, 17, 1m, 4 },
                    { 42, 18, 1m, 4 },
                    { 43, 19, 1m, 4 },
                    { 44, 20, 1m, 4 },
                    { 45, 21, 1m, 4 },
                    { 46, 30, 1m, 4 },
                    { 47, 31, 1m, 4 },
                    { 48, 38, 1m, 4 },
                    { 49, 41, 1m, 4 },
                    { 50, 1, 1m, 5 },
                    { 51, 2, 1m, 5 },
                    { 52, 3, 1m, 5 },
                    { 53, 7, 1m, 5 },
                    { 54, 8, 1m, 5 },
                    { 55, 9, 1m, 5 },
                    { 56, 10, 1m, 5 },
                    { 57, 11, 1m, 5 },
                    { 58, 12, 1m, 5 },
                    { 59, 18, 1m, 5 },
                    { 60, 20, 1m, 5 },
                    { 61, 22, 1m, 5 },
                    { 62, 30, 1m, 5 },
                    { 63, 38, 1m, 5 },
                    { 64, 41, 1m, 5 },
                    { 65, 1, 1m, 6 },
                    { 66, 2, 1m, 6 },
                    { 67, 3, 1m, 6 },
                    { 68, 8, 1m, 6 },
                    { 69, 10, 1m, 6 },
                    { 70, 11, 1m, 6 },
                    { 71, 12, 1m, 6 },
                    { 72, 24, 1m, 6 },
                    { 73, 26, 1m, 6 },
                    { 74, 27, 1m, 6 },
                    { 75, 31, 1m, 6 },
                    { 76, 41, 1m, 6 },
                    { 77, 42, 1m, 6 },
                    { 78, 43, 1m, 6 },
                    { 79, 44, 1m, 6 },
                    { 80, 45, 1m, 6 },
                    { 81, 3, 1m, 7 },
                    { 82, 7, 1m, 7 },
                    { 83, 8, 1m, 7 },
                    { 84, 9, 1m, 7 },
                    { 85, 10, 1m, 7 },
                    { 86, 11, 1m, 7 },
                    { 87, 12, 1m, 7 },
                    { 88, 13, 1m, 7 },
                    { 89, 14, 1m, 7 },
                    { 90, 18, 1m, 7 },
                    { 91, 19, 1m, 7 },
                    { 92, 20, 1m, 7 },
                    { 93, 31, 1m, 7 },
                    { 94, 38, 1m, 7 },
                    { 95, 41, 1m, 7 },
                    { 96, 44, 1m, 7 },
                    { 97, 47, 1m, 7 },
                    { 98, 48, 1m, 7 },
                    { 99, 49, 1m, 7 },
                    { 100, 50, 1m, 7 },
                    { 101, 2, 1m, 8 },
                    { 102, 3, 1m, 8 },
                    { 103, 10, 1m, 8 },
                    { 104, 11, 1m, 8 },
                    { 105, 12, 1m, 8 },
                    { 106, 14, 1m, 8 },
                    { 107, 15, 1m, 8 },
                    { 108, 16, 1m, 8 },
                    { 109, 19, 1m, 8 },
                    { 110, 20, 1m, 8 },
                    { 111, 27, 1m, 8 },
                    { 112, 31, 1m, 8 },
                    { 113, 41, 1m, 8 },
                    { 114, 2, 1m, 9 },
                    { 115, 9, 1m, 9 },
                    { 116, 10, 1m, 9 },
                    { 117, 11, 1m, 9 },
                    { 118, 12, 1m, 9 },
                    { 119, 27, 1m, 9 },
                    { 120, 30, 1m, 9 },
                    { 121, 31, 1m, 9 },
                    { 122, 38, 1m, 9 },
                    { 123, 42, 1m, 9 },
                    { 124, 43, 1m, 9 },
                    { 125, 3, 1m, 10 },
                    { 126, 15, 1m, 10 },
                    { 127, 25, 1m, 10 },
                    { 128, 27, 1m, 10 },
                    { 129, 28, 1m, 10 },
                    { 130, 3, 1m, 11 },
                    { 131, 6, 1m, 11 },
                    { 132, 9, 1m, 11 },
                    { 133, 10, 1m, 11 },
                    { 134, 11, 1m, 11 },
                    { 135, 12, 1m, 11 },
                    { 136, 18, 1m, 11 },
                    { 137, 19, 1m, 11 },
                    { 138, 20, 1m, 11 },
                    { 139, 31, 1m, 11 },
                    { 140, 38, 1m, 11 },
                    { 141, 44, 1m, 11 },
                    { 142, 10, 1m, 12 },
                    { 143, 11, 1m, 12 },
                    { 144, 12, 1m, 12 },
                    { 145, 13, 1m, 12 },
                    { 146, 14, 1m, 12 },
                    { 147, 27, 1m, 12 },
                    { 148, 45, 1m, 12 },
                    { 149, 46, 1m, 12 },
                    { 150, 2, 1m, 13 },
                    { 151, 4, 1m, 13 },
                    { 152, 8, 1m, 13 },
                    { 153, 9, 1m, 13 },
                    { 154, 10, 1m, 13 },
                    { 155, 11, 1m, 13 },
                    { 156, 12, 1m, 13 },
                    { 157, 29, 1m, 13 },
                    { 158, 30, 1m, 13 },
                    { 159, 34, 1m, 13 },
                    { 160, 41, 1m, 13 },
                    { 161, 2, 1m, 14 },
                    { 162, 4, 1m, 14 },
                    { 163, 8, 1m, 14 },
                    { 164, 9, 1m, 14 },
                    { 165, 11, 1m, 14 },
                    { 166, 12, 1m, 14 },
                    { 167, 15, 1m, 14 },
                    { 168, 35, 1m, 14 },
                    { 169, 36, 1m, 14 },
                    { 170, 2, 1m, 15 },
                    { 171, 3, 1m, 15 },
                    { 172, 8, 1m, 15 },
                    { 173, 9, 1m, 15 },
                    { 174, 10, 1m, 15 },
                    { 175, 11, 1m, 15 },
                    { 176, 12, 1m, 15 },
                    { 177, 16, 1m, 15 },
                    { 178, 30, 1m, 15 },
                    { 179, 33, 1m, 15 },
                    { 180, 37, 1m, 15 },
                    { 181, 2, 1m, 16 },
                    { 182, 3, 1m, 16 },
                    { 183, 4, 1m, 16 },
                    { 184, 7, 1m, 16 },
                    { 185, 8, 1m, 16 },
                    { 186, 9, 1m, 16 },
                    { 187, 10, 1m, 16 },
                    { 188, 11, 1m, 16 },
                    { 189, 12, 1m, 16 },
                    { 190, 18, 1m, 16 },
                    { 191, 19, 1m, 16 },
                    { 192, 20, 1m, 16 },
                    { 193, 30, 1m, 16 },
                    { 194, 31, 1m, 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenus_RecipeId",
                table: "DailyMenus",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementDetails_ItemId",
                table: "ProcurementDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementDetails_ProcurementId",
                table: "ProcurementDetails",
                column: "ProcurementId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_ItemId",
                table: "RecipeItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeId",
                table: "RecipeItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_ItemId",
                table: "Requisitions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_DailyMenuId",
                table: "SaleDetails",
                column: "DailyMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_SaleHeaderId",
                table: "SaleDetails",
                column: "SaleHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ItemId",
                table: "Stocks",
                column: "ItemId");
        }

        /// <inheritdoc />
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
                name: "ProcurementDetails");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropTable(
                name: "SaleDetails");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Procurements");

            migrationBuilder.DropTable(
                name: "DailyMenus");

            migrationBuilder.DropTable(
                name: "SaleHeader");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
