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
    DateTime CreatedDate,
    ProductDetailDto? ProductDetail);

public sealed record ProductDetailDto(
    Guid Id,
    string? Color,
    string? Size,
    string? Material,
    string? Brand,
    string? Model,
    string? Warranty,
    string? Specifications,
    string? AdditionalInformation,
    decimal? Weight,
    string? WeightUnit,
    string? Dimensions,
    int? StockCode,
    string? Barcode); 