using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetOrderCount;

public sealed record GetOrderCountQuery : IRequest<int>; 