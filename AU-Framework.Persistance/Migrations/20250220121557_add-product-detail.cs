using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addproductdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("09d1a752-d520-4d0b-a9ea-aa671d7f97bf"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("11421fe5-8a1c-4ce7-8ba8-a9f434a2264b"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("41bc25fe-1005-4cc4-b281-eef63b6eca67"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("515ecd19-9c40-4add-8763-981908bd7a49"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("a6cc3fd0-8a47-4ef7-8567-07e4f463d727"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d1556a52-3800-4499-a501-489c280cfdd8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3ce4a262-59a7-4d4b-a424-ea3000794d50"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("61c00b4a-3af4-44d1-8113-121de315d814"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e05d6f0c-8229-4454-b3fc-d3f5478f4392"));

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Warranty = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightUnit = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    StockCode = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_Barcode",
                table: "ProductDetails",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_StockCode",
                table: "ProductDetails",
                column: "StockCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("09d1a752-d520-4d0b-a9ea-aa671d7f97bf"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1834), null, "Sipariş onaylandı", 2, true, false, "Onaylandı", null },
                    { new Guid("11421fe5-8a1c-4ce7-8ba8-a9f434a2264b"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1840), null, "Sipariş kargoya verildi", 4, true, false, "Kargoda", null },
                    { new Guid("41bc25fe-1005-4cc4-b281-eef63b6eca67"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1844), null, "Sipariş tamamlandı", 5, true, false, "Tamamlandı", null },
                    { new Guid("515ecd19-9c40-4add-8763-981908bd7a49"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1846), null, "Sipariş iptal edildi", 6, true, false, "İptal Edildi", null },
                    { new Guid("a6cc3fd0-8a47-4ef7-8567-07e4f463d727"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1837), null, "Sipariş hazırlanıyor", 3, true, false, "Hazırlanıyor", null },
                    { new Guid("d1556a52-3800-4499-a501-489c280cfdd8"), new DateTime(2025, 2, 19, 9, 13, 1, 378, DateTimeKind.Utc).AddTicks(1831), null, "Sipariş onay bekliyor", 1, true, false, "Beklemede", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3ce4a262-59a7-4d4b-a424-ea3000794d50"), new DateTime(2025, 2, 19, 9, 13, 1, 376, DateTimeKind.Utc).AddTicks(6575), null, "Kullanıcı", false, "User", null },
                    { new Guid("61c00b4a-3af4-44d1-8113-121de315d814"), new DateTime(2025, 2, 19, 9, 13, 1, 376, DateTimeKind.Utc).AddTicks(6572), null, "Yönetici", false, "Manager", null },
                    { new Guid("e05d6f0c-8229-4454-b3fc-d3f5478f4392"), new DateTime(2025, 2, 19, 9, 13, 1, 376, DateTimeKind.Utc).AddTicks(6569), null, "Sistem Yöneticisi", false, "Admin", null }
                });
        }
    }
}
