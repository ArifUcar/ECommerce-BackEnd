using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Domain.Entities;
using AU_Framework.Domain.Dtos;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.DeleteOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrderStatus;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;

namespace AU_Framework.Application.Services;

public interface IOrderService
{
    Task CreateAsync(CreateOrderCommand request, CancellationToken cancellationToken);
    Task<List<OrderDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<OrderDto>> GetUserOrdersAsync(CancellationToken cancellationToken);
    Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateOrderCommand request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteOrderCommand request, CancellationToken cancellationToken);
    Task CancelOrderAsync(Guid orderId, CancellationToken cancellationToken);
    Task<int> GetOrderCountAsync(CancellationToken cancellationToken);
    Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken);
    Task UpdateOrderStatusAsync(UpdateOrderStatusCommand request, CancellationToken cancellationToken);
} 