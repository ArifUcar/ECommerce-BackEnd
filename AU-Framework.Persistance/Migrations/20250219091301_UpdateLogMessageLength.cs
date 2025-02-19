using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogMessageLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("035a77bc-e138-41c9-8d90-47d3325a8e1d"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("24ab2a7e-5f05-4b08-acf6-083ac4cabc45"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("43336c79-f6c9-4ef2-a3cb-fe831246c7bc"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d48a472a-5418-474e-9a91-9313ef33f43e"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("dc3d82dc-ad36-49cb-9ab8-175a6231d49a"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("ded4a5a7-32f7-4b7a-ba42-b6a5ecc9c53d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("14ae6f60-1ecb-4bdf-9a5f-8ac7a9f4fab2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4e8774a4-d7c1-4838-b61c-e8565aae3931"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e49ba8d0-aca5-454d-a222-595f22163469"));

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Logs",
                type: "nvarchar(MAX)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Logs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldMaxLength: 4000);

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("035a77bc-e138-41c9-8d90-47d3325a8e1d"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9507), null, "Sipariş onaylandı", 2, true, false, "Onaylandı", null },
                    { new Guid("24ab2a7e-5f05-4b08-acf6-083ac4cabc45"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9510), null, "Sipariş hazırlanıyor", 3, true, false, "Hazırlanıyor", null },
                    { new Guid("43336c79-f6c9-4ef2-a3cb-fe831246c7bc"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9517), null, "Sipariş kargoya verildi", 4, true, false, "Kargoda", null },
                    { new Guid("d48a472a-5418-474e-9a91-9313ef33f43e"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9519), null, "Sipariş tamamlandı", 5, true, false, "Tamamlandı", null },
                    { new Guid("dc3d82dc-ad36-49cb-9ab8-175a6231d49a"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9504), null, "Sipariş onay bekliyor", 1, true, false, "Beklemede", null },
                    { new Guid("ded4a5a7-32f7-4b7a-ba42-b6a5ecc9c53d"), new DateTime(2025, 2, 19, 8, 45, 19, 322, DateTimeKind.Utc).AddTicks(9522), null, "Sipariş iptal edildi", 6, true, false, "İptal Edildi", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("14ae6f60-1ecb-4bdf-9a5f-8ac7a9f4fab2"), new DateTime(2025, 2, 19, 8, 45, 19, 321, DateTimeKind.Utc).AddTicks(3842), null, "Kullanıcı", false, "User", null },
                    { new Guid("4e8774a4-d7c1-4838-b61c-e8565aae3931"), new DateTime(2025, 2, 19, 8, 45, 19, 321, DateTimeKind.Utc).AddTicks(3828), null, "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("e49ba8d0-aca5-454d-a222-595f22163469"), new DateTime(2025, 2, 19, 8, 45, 19, 321, DateTimeKind.Utc).AddTicks(3831), null, "Yönetici", false, "Manager", null }
                });
        }
    }
}
