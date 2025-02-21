using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetDiscountedProducts;

public sealed record GetDiscountedProductsQuery() : IRequest<List<ProductDto>>;