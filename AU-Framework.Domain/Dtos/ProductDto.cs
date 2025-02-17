namespace AU_Framework.Domain.Dtos;

public sealed record ProductDto(
    Guid Id,
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity,
    string CategoryName,
    Guid CategoryId
); 