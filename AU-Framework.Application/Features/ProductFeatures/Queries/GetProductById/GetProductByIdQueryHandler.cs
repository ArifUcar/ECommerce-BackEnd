using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductService _productService;

    public GetProductByIdQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetByIdAsync(request.Id, cancellationToken);
    }
} 