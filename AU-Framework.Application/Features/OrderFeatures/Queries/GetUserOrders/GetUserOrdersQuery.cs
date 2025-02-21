using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetUserOrders;

public sealed record GetUserOrdersQuery : IRequest<List<OrderDto>>; 