using Microsoft.EntityFrameworkCore.Migrations;

namespace AU_Framework.Persistance.Migrations;

public partial class AddOrderStatusName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "OrderStatuses",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        // Varsayılan durumları ekleyelim
        migrationBuilder.InsertData(
            table: "OrderStatuses",
            columns: new[] { "Id", "Name", "Description", "CreatedDate", "IsDeleted" },
            values: new object[,]
            {
                { Guid.NewGuid(), "Beklemede", "Sipariş onay bekliyor", DateTime.UtcNow, false },
                { Guid.NewGuid(), "Onaylandı", "Sipariş onaylandı", DateTime.UtcNow, false },
                { Guid.NewGuid(), "Hazırlanıyor", "Sipariş hazırlanıyor", DateTime.UtcNow, false },
                { Guid.NewGuid(), "Kargoda", "Sipariş kargoya verildi", DateTime.UtcNow, false },
                { Guid.NewGuid(), "Tamamlandı", "Sipariş tamamlandı", DateTime.UtcNow, false },
                { Guid.NewGuid(), "İptal Edildi", "Sipariş iptal edildi", DateTime.UtcNow, false }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Name",
            table: "OrderStatuses");
    }
} 