namespace AU_Framework.Domain.Dtos;

public sealed record ProductDto(
    Guid Id,
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId,
    string CategoryName,
    string? ImagePath,
    string? Base64Image,
    DateTime CreatedDate); 