using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductById;

public sealed record GetProductByIdQuery(
    Guid Id
) : IRequest<ProductDto>; 