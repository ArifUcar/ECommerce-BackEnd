using AU_Framework.Application.Repository;
using AU_Framework.Domain.Dtos;
using AU_Framework.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetStockSummary;

public sealed class GetStockSummaryQueryHandler : IRequestHandler<GetStockSummaryQuery, ProductStockSummaryDto>
{
    private readonly IRepository<Product> _productRepository;
    private const int LOW_STOCK_THRESHOLD = 10;

    public GetStockSummaryQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductStockSummaryDto> Handle(GetStockSummaryQuery request, CancellationToken cancellationToken)
    {
        var query = await _productRepository.GetAllAsync(cancellationToken);
        var products = query.Where(p => !p.IsDeleted);

        var totalProducts = await products.CountAsync(cancellationToken);
        var totalStock = await products.SumAsync(p => p.StockQuantity, cancellationToken);
        var outOfStock = await products.CountAsync(p => p.StockQuantity == 0, cancellationToken);
        var lowStock = await products.CountAsync(p => p.StockQuantity > 0 && p.StockQuantity <= LOW_STOCK_THRESHOLD, cancellationToken);

        return new ProductStockSummaryDto(
            TotalProducts: totalProducts,
            TotalStockQuantity: totalStock,
            OutOfStockProducts: outOfStock,
            LowStockProducts: lowStock);
    }
}