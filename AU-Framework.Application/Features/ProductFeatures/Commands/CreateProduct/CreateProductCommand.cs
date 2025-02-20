using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string ProductName,
    string Description,
    decimal Price,
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