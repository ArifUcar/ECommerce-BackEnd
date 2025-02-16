using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class productupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a8c45a6e-a370-43c0-bf22-b0fe1daec5ec"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6504), null, "Yönetici", false, "Manager", null },
                    { new Guid("d68cc4e9-67b9-481d-80f1-4a9a392630c2"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6501), null, "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("e958c8e7-ab80-4b5a-9584-d2bc2eb1fb10"), new DateTime(2025, 2, 16, 19, 28, 4, 159, DateTimeKind.Utc).AddTicks(6519), null, "Kullanıcı", false, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
