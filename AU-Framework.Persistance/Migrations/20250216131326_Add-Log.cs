using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d766e679-dad5-4342-be96-0d252c7665a8"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("efa4d2d3-070b-4223-a7e9-b17bc1a18729"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("f539c0f4-0602-45bc-ba77-2771adb15515"));

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExecutionTime = table.Column<long>(type: "bigint", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("ae47251e-29f1-471f-9125-48f2c60503a3"), new DateTime(2025, 2, 16, 13, 13, 25, 774, DateTimeKind.Utc).AddTicks(1054), "Kullanıcı", false, "User", null },
                    { new Guid("afd1e7fe-6d44-4b81-860e-71faa79556e9"), new DateTime(2025, 2, 16, 13, 13, 25, 774, DateTimeKind.Utc).AddTicks(1038), "Yönetici", false, "Manager", null },
                    { new Guid("b4c47440-7dd3-411a-abea-1ed5df0475c1"), new DateTime(2025, 2, 16, 13, 13, 25, 774, DateTimeKind.Utc).AddTicks(1030), "Sistem Yöneticisi", false, "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("ae47251e-29f1-471f-9125-48f2c60503a3"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("afd1e7fe-6d44-4b81-860e-71faa79556e9"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b4c47440-7dd3-411a-abea-1ed5df0475c1"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("d766e679-dad5-4342-be96-0d252c7665a8"), new DateTime(2025, 2, 16, 12, 59, 38, 256, DateTimeKind.Utc).AddTicks(2710), "Kullanıcı", false, "User", null },
                    { new Guid("efa4d2d3-070b-4223-a7e9-b17bc1a18729"), new DateTime(2025, 2, 16, 12, 59, 38, 256, DateTimeKind.Utc).AddTicks(2693), "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("f539c0f4-0602-45bc-ba77-2771adb15515"), new DateTime(2025, 2, 16, 12, 59, 38, 256, DateTimeKind.Utc).AddTicks(2699), "Yönetici", false, "Manager", null }
                });
        }
    }
}
