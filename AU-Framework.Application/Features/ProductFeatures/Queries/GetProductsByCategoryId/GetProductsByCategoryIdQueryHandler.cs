using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductsByCategoryId;

public sealed class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQuery, IList<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProductsByCategoryIdQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IList<ProductDto>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetByCategoryIdAsync(request.CategoryId, cancellationToken);
    }
} 