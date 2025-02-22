namespace AU_Framework.Domain.Dtos;

public sealed record OrderDto(
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
    DateTime? DeleteDate,
    List<OrderDetailResponse> OrderDetails);

public sealed record OrderDetailResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    DateTime? DeleteDate
    
);