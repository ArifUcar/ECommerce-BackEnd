using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetTotalRevenue;

public sealed record GetTotalRevenueQuery : IRequest<decimal>; 