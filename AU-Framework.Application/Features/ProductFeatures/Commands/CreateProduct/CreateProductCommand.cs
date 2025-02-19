using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId,
    string? Base64Image
) : IRequest<MessageResponse>; 