using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;

public sealed record CreateOrderCommand : IRequest<MessageResponse>
{
    public Guid OrderStatusId { get; init; }
    public decimal TotalAmount { get; init; }
    public List<OrderDetailCommand> OrderDetails { get; init; }
}

public sealed record OrderDetailCommand
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}

