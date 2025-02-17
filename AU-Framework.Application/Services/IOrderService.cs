using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Domain.Entities;
using AU_Framework.Domain.Dtos;

namespace AU_Framework.Application.Services;

public interface IOrderService
{
    Task CreateAsync(CreateOrderCommand request, CancellationToken cancellationToken);
    Task<List<OrderDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
} 