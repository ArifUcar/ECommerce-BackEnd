using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetAllProducts;

public sealed record GetAllProductsQuery() : IRequest<IList<Product>>; 