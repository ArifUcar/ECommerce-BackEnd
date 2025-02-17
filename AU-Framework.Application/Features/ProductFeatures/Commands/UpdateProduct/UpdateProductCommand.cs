using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string ProductName,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId
) : IRequest<MessageResponse>; 