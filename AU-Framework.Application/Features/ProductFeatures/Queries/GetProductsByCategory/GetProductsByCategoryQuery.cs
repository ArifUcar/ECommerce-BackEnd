using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<List<ProductDto>>;