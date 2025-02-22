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
    // Sipariş veren kişi bilgileri
    string CustomerName,
    string CustomerPhone,
    // Adres bilgileri
    string ShippingAddress,
    string City,
    string District,
    string ZipCode,
    // Audit bilgileri
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    bool IsDeleted,
    List<OrderDetailResponse> OrderDetails
);

public sealed record OrderDetailResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal,
    // Audit bilgileri
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    bool IsDeleted
); 