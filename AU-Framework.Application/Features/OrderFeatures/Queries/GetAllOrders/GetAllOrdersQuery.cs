using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IRequest<List<OrderDto>>;

