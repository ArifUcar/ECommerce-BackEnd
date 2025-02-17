using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateorderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Orders_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a8c45a6e-a370-43c0-bf22-b0fe1daec5ec"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d68cc4e9-67b9-481d-80f1-4a9a392630c2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e958c8e7-ab80-4b5a-9584-d2bc2eb1fb10"));

            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderStatuses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrderStatuses",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0e35a7fc-877e-45ee-a9b8-7c40c4d34bc9"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9325), null, "Sipariş hazırlanıyor", 3, true, false, "Hazırlanıyor", null },
                    { new Guid("33e449a6-4945-460a-86b3-4ff27b931a9e"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9328), null, "Sipariş kargoya verildi", 4, true, false, "Kargoda", null },
                    { new Guid("7f345ec3-8b22-46d8-a1d3-769a9701601c"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9335), null, "Sipariş iptal edildi", 6, true, false, "İptal Edildi", null },
                    { new Guid("e61082e4-8fb7-4141-ac4d-4fb873cdd2bf"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9332), null, "Sipariş tamamlandı", 5, true, false, "Tamamlandı", null },
                    { new Guid("f1753d6b-f539-444b-83bf-686fdbb3767e"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9318), null, "Sipariş onay bekliyor", 1, true, false, "Beklemede", null },
                    { new Guid("f7c39026-21a5-4496-b324-78cfbc48bde5"), new DateTime(2025, 2, 16, 21, 55, 38, 446, DateTimeKind.Utc).AddTicks(9322), null, "Sipariş onaylandı", 2, true, false, "Onaylandı", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("4532e32b-80a5-4a07-baa3-ab6cf7e7c729"), new DateTime(2025, 2, 16, 21, 55, 38, 444, DateTimeKind.Utc).AddTicks(9928), null, "Yönetici", false, "Manager", null },
                    { new Guid("ce02b038-3406-417d-88ff-7bec42e4298b"), new DateTime(2025, 2, 16, 21, 55, 38, 444, DateTimeKind.Utc).AddTicks(9936), null, "Kullanıcı", false, "User", null },
                    { new Guid("f8e59d71-1292-4bf8-bb4e-d6556706fdc4"), new DateTime(2025, 2, 16, 21, 55, 38, 444, DateTimeKind.Utc).AddTicks(9925), null, "Sistem Yöneticisi", false, "Admin", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("0e35a7fc-877e-45ee-a9b8-7c40c4d34bc9"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("33e449a6-4945-460a-86b3-4ff27b931a9e"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("7f345ec3-8b22-46d8-a1d3-769a9701601c"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e61082e4-8fb7-4141-ac4d-4fb873cdd2bf"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("f1753d6b-f539-444b-83bf-686fdbb3767e"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("f7c39026-21a5-4496-b324-78cfbc48bde5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4532e32b-80a5-4a07-baa3-ab6cf7e7c729"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ce02b038-3406-417d-88ff-7bec42e4298b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f8e59d71-1292-4bf8-bb4e-d6556706fdc4"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                newName: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "OrderStatus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a8c45a6e-a370-43c0-bf22-b0fe1daec5ec"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6504), null, "Yönetici", false, "Manager", null },
                    { new Guid("d68cc4e9-67b9-481d-80f1-4a9a392630c2"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6501), null, "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("e958c8e7-ab80-4b5a-9584-d2bc2eb1fb10"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6519), null, "Kullanıcı", false, "User", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Orders_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Products_ProductId",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
