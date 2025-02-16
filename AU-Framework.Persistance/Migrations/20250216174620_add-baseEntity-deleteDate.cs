using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addbaseEntitydeleteDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24b89c4c-7fce-4e8d-b0a1-e6e1a01a8b26"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("929907ad-611d-4340-9779-5a4a8fde5efd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b30e8254-3389-431b-b2a7-112551e17b60"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "OrderStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "OrderDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("3518f102-967d-4792-aab4-ef427ff8c844"), new DateTime(2025, 2, 16, 17, 46, 20, 325, DateTimeKind.Utc).AddTicks(2773), null, "Yönetici", false, "Manager", null },
                    { new Guid("527ecd24-1cae-496c-8576-115248f52ddf"), new DateTime(2025, 2, 16, 17, 46, 20, 325, DateTimeKind.Utc).AddTicks(2764), null, "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("56ebe739-7d85-4e1a-acc3-249427a49712"), new DateTime(2025, 2, 16, 17, 46, 20, 325, DateTimeKind.Utc).AddTicks(2778), null, "Kullanıcı", false, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3518f102-967d-4792-aab4-ef427ff8c844"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("527ecd24-1cae-496c-8576-115248f52ddf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("56ebe739-7d85-4e1a-acc3-249427a49712"));

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "OrderStatus");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("24b89c4c-7fce-4e8d-b0a1-e6e1a01a8b26"), new DateTime(2025, 2, 16, 15, 1, 38, 143, DateTimeKind.Utc).AddTicks(5028), "Yönetici", false, "Manager", null },
                    { new Guid("929907ad-611d-4340-9779-5a4a8fde5efd"), new DateTime(2025, 2, 16, 15, 1, 38, 143, DateTimeKind.Utc).AddTicks(5032), "Kullanıcı", false, "User", null },
                    { new Guid("b30e8254-3389-431b-b2a7-112551e17b60"), new DateTime(2025, 2, 16, 15, 1, 38, 143, DateTimeKind.Utc).AddTicks(5024), "Sistem Yöneticisi", false, "Admin", null }
                });
        }
    }
}
