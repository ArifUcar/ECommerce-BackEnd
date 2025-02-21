using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class orderstatusroleaddveri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "OrderStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OrderStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "DisplayOrder", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921e"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3670), null, null, 1, true, "Beklemede", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3671) },
                    { new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921f"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3675), null, null, 2, true, "Onaylandı", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3675) },
                    { new Guid("af7579ee-4af9-4b71-9ada-7f792f76921a"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3688), null, null, 3, true, "Hazırlanıyor", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3688) },
                    { new Guid("bf7579ee-4af9-4b71-9ada-7f792f76921b"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3691), null, null, 4, true, "Kargoya Verildi", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3691) },
                    { new Guid("cf7579ee-4af9-4b71-9ada-7f792f76921c"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3693), null, null, 5, true, "Tamamlandı", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3694) },
                    { new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3696), null, null, 6, true, "İptal Edildi", new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3696) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921c"), new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7912), null, null, "Admin", new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7912) },
                    { new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921d"), new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7918), null, null, "User", new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7918) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_Name",
                table: "OrderStatuses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_Name",
                table: "OrderStatuses");

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921e"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921f"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("af7579ee-4af9-4b71-9ada-7f792f76921a"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bf7579ee-4af9-4b71-9ada-7f792f76921b"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cf7579ee-4af9-4b71-9ada-7f792f76921c"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "OrderStatuses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OrderStatuses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
