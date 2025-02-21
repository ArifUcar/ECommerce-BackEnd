using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;

public sealed record CreateProductCommand : IRequest<MessageResponse>
{
    public string ProductName { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public decimal? DiscountedPrice { get; init; }
    public decimal? DiscountRate { get; init; }
    public DateTime? DiscountStartDate { get; init; }
    public DateTime? DiscountEndDate { get; init; }
    public int StockQuantity { get; init; }
    public Guid CategoryId { get; init; }
    public string? Base64Image { get; init; }
    public ProductDetailDto ProductDetail { get; init; }
}

