using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductsByCategoryId;

public sealed record GetProductsByCategoryIdQuery(
    Guid CategoryId
) : IRequest<IList<ProductDto>>; 