using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AU_Framework.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderShippingInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921e"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3904), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3905) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921f"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3908), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3908) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("af7579ee-4af9-4b71-9ada-7f792f76921a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3911), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3911) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bf7579ee-4af9-4b71-9ada-7f792f76921b"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3914), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3914) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cf7579ee-4af9-4b71-9ada-7f792f76921c"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3958), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3958) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 2, 21, 14, 19, 42, 22, DateTimeKind.Utc).AddTicks(3971) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921c"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 20, DateTimeKind.Utc).AddTicks(7525), new DateTime(2025, 2, 21, 14, 19, 42, 20, DateTimeKind.Utc).AddTicks(7525) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 14, 19, 42, 20, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 2, 21, 14, 19, 42, 20, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921e"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3670), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3671) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921f"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3675), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3675) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("af7579ee-4af9-4b71-9ada-7f792f76921a"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3688), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3688) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bf7579ee-4af9-4b71-9ada-7f792f76921b"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3691), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3691) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cf7579ee-4af9-4b71-9ada-7f792f76921c"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3693), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3694) });

            migrationBuilder.UpdateData(
                table: "OrderStatuses",
                keyColumn: "Id",
                keyValue: new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3696), new DateTime(2025, 2, 21, 13, 24, 18, 985, DateTimeKind.Utc).AddTicks(3696) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8f7579ee-4af9-4b71-9ada-7f792f76921c"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7912), new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7912) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9f7579ee-4af9-4b71-9ada-7f792f76921d"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7918), new DateTime(2025, 2, 21, 13, 24, 18, 983, DateTimeKind.Utc).AddTicks(7918) });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
