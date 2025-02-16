using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductById;

public sealed record GetProductByIdQuery(
    string Id
) : IRequest<Product>; 