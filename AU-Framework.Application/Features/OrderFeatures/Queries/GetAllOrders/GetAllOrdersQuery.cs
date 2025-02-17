using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IRequest<List<GetAllOrdersQueryResponse>>;

public sealed record GetAllOrdersQueryResponse(
    Guid Id,
    Guid UserId,
    string UserFullName,
    DateTime OrderDate,
    decimal TotalAmount,
    string OrderStatus,
    List<OrderDetailDto> OrderDetails
); 