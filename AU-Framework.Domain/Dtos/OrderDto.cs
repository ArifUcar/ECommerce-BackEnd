namespace AU_Framework.Domain.Dtos;

public sealed record OrderDto(
    Guid Id,
    Guid UserId,
    string UserFullName,
    DateTime OrderDate,
    decimal TotalAmount,
    string OrderStatus,
    List<OrderDetailDto> OrderDetails); 