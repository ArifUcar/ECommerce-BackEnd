using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductsByCategory;

public sealed class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, List<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProductsByCategoryQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = await _productService.GetByCategoryIdAsync(request.CategoryId, cancellationToken);
        return products.ToList();
    }
}