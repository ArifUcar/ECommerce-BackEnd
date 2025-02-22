namespace AU_Framework.Domain.Dtos;

public sealed record OrderDto(
     Guid Id,
    Guid UserId,
    string UserFullName,
    DateTime OrderDate,
    decimal TotalAmount,
    string OrderStatus,
    // Sipariþ veren kiþi bilgileri
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
    List<OrderDetailResponse> OrderDetails);
public sealed record OrderDetailResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal,
    // Audit bilgileri
    DateTime CreatedDate,
    DateTime? UpdatedDate
 
);