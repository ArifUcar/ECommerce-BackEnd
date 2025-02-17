using Microsoft.EntityFrameworkCore.Migrations;

namespace AU_Framework.Persistance.Migrations;

public partial class UpdateOrderStatus : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
   
        migrationBuilder.DropColumn(
            name: "StatusName",
            table: "OrderStatuses");

      
        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "OrderStatuses",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false);

        migrationBuilder.AddColumn<string>(
            name: "Description",
            table: "OrderStatuses",
            type: "nvarchar(250)",
            maxLength: 250,
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "OrderStatuses",
            type: "bit",
            nullable: false,
            defaultValue: true);

        migrationBuilder.AddColumn<int>(
            name: "DisplayOrder",
            table: "OrderStatuses",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "OrderStatuses");

        migrationBuilder.DropColumn(
            name: "Description",
            table: "OrderStatuses");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "OrderStatuses");

        migrationBuilder.DropColumn(
            name: "DisplayOrder",
            table: "OrderStatuses");

        migrationBuilder.AddColumn<string>(
            name: "StatusName",
            table: "OrderStatuses",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false);
    }
} 