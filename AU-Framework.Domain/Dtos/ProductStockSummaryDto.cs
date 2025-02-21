namespace AU_Framework.Domain.Dtos;

public sealed record ProductStockSummaryDto(
    int TotalProducts,
    int TotalStockQuantity,
    int OutOfStockProducts,
    int LowStockProducts); // Örneğin stok sayısı 10'un altında olanlar 