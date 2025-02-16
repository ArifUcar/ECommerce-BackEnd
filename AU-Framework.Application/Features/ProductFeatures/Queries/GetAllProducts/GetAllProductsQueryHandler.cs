using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetAllProducts;

public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
{
    private readonly IProductService _productService;

    public GetAllProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetAllAsync(cancellationToken);
    }
} 