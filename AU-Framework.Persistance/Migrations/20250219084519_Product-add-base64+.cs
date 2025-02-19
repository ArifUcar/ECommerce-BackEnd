using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Productaddbase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("14cedc63-741e-4c0e-9b01-f2059580af56"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2f38106f-5a1f-4443-8b35-9c5f936fc6b5"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("54342441-d9fa-44ab-ab9b-fa0131f6449d"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("6119666c-da0d-42a7-85d7-89ead4d70a7a"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("93ae27e4-5c9b-4d12-8a77-dfe2ed260a20"));

            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("ed13d4c2-f572-4afb-a9cf-107e555fefed"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01ed10c5-80e9-440a-9f69-2882fc84b231"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92610536-21fe-4119-9920-d18d350c4fe6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bf3ab7f5-20dd-45ff-ac73-5d8968a149a9"));

            migrationBuilder.AddColumn<string>(
                name: "Base64Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Base64Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("14cedc63-741e-4c0e-9b01-f2059580af56"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2903), null, "Sipariş onaylandı", 2, true, false, "Onaylandı", null },
                    { new Guid("2f38106f-5a1f-4443-8b35-9c5f936fc6b5"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2906), null, "Sipariş hazırlanıyor", 3, true, false, "Hazırlanıyor", null },
                    { new Guid("54342441-d9fa-44ab-ab9b-fa0131f6449d"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2900), null, "Sipariş onay bekliyor", 1, true, false, "Beklemede", null },
                    { new Guid("6119666c-da0d-42a7-85d7-89ead4d70a7a"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2914), null, "Sipariş tamamlandı", 5, true, false, "Tamamlandı", null },
                    { new Guid("93ae27e4-5c9b-4d12-8a77-dfe2ed260a20"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2916), null, "Sipariş iptal edildi", 6, true, false, "İptal Edildi", null },
                    { new Guid("ed13d4c2-f572-4afb-a9cf-107e555fefed"), new DateTime(2025, 2, 19, 8, 40, 58, 618, DateTimeKind.Utc).AddTicks(2909), null, "Sipariş kargoya verildi", 4, true, false, "Kargoda", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "DeleteDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("01ed10c5-80e9-440a-9f69-2882fc84b231"), new DateTime(2025, 2, 19, 8, 40, 58, 616, DateTimeKind.Utc).AddTicks(3307), null, "Kullanıcı", false, "User", null },
                    { new Guid("92610536-21fe-4119-9920-d18d350c4fe6"), new DateTime(2025, 2, 19, 8, 40, 58, 616, DateTimeKind.Utc).AddTicks(3299), null, "Sistem Yöneticisi", false, "Admin", null },
                    { new Guid("bf3ab7f5-20dd-45ff-ac73-5d8968a149a9"), new DateTime(2025, 2, 19, 8, 40, 58, 616, DateTimeKind.Utc).AddTicks(3303), null, "Yönetici", false, "Manager", null }
                });
        }
    }
}
