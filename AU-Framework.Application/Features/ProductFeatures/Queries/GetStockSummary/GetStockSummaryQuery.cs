using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetStockSummary;

public sealed record GetStockSummaryQuery : IRequest<ProductStockSummaryDto>; 