using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetDiscountedProducts;

public sealed class GetDiscountedProductsQueryHandler : IRequestHandler<GetDiscountedProductsQuery, List<ProductDto>>
{
    private readonly IProductService _productService;

    public GetDiscountedProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductDto>> Handle(GetDiscountedProductsQuery request, CancellationToken cancellationToken)
    {
        var allProducts = await _productService.GetAllAsync(cancellationToken);
        return allProducts.Where(p => p.IsDiscounted).ToList();
    }
}