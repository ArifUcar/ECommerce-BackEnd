using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string ProductName,
    string Description,
    decimal Price,
    decimal? DiscountedPrice,
    decimal? DiscountRate,
    DateTime? DiscountStartDate,
    DateTime? DiscountEndDate,
    int StockQuantity,
    Guid CategoryId,
    string? Base64Image,
    // ProductDetail özellikleri
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
    string? Barcode
) : IRequest<MessageResponse>; 