using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;

public sealed record UpdateOrderCommand(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    Guid OrderStatusId,
    decimal TotalAmount,
    // Sipariş veren kişi bilgileri
    string CustomerName,
    string CustomerPhone,
    // Adres bilgileri
    string ShippingAddress,
    string City,
    string District,
    string ZipCode,
    // Sipariş detayları
    List<UpdateOrderDetailCommand> OrderDetails
) : IRequest<MessageResponse>;

public sealed record UpdateOrderDetailCommand(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal
);
    
    

