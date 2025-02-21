namespace AU_Framework.Domain.Dtos;

public sealed record OrderDetailDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice); 