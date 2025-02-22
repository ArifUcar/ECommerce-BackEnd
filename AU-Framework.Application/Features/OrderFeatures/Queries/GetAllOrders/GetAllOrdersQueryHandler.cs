using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;

public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetAllAsync(cancellationToken);
    }
} 