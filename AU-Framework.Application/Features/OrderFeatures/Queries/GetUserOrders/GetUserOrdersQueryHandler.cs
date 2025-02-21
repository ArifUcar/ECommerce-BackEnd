using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetUserOrders;

public sealed class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, List<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetUserOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<List<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetUserOrdersAsync(cancellationToken);
    }
} 