using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;

public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<GetAllOrdersQueryResponse>>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<List<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetAllAsync(cancellationToken);

        return orders.Select(order => new GetAllOrdersQueryResponse(
            Id: order.Id,
            UserId: order.UserId,
            UserFullName: order.UserFullName,
            OrderDate: order.OrderDate,
            TotalAmount: order.TotalAmount,
            OrderStatus: order.OrderStatus,
            OrderDetails: order.OrderDetails  // Doğrudan DTO'dan gelen OrderDetails'i kullanıyoruz
        )).ToList();
    }
} 