using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetAllProducts;

public sealed record GetAllProductsQuery() : IRequest<List<ProductDto>>;