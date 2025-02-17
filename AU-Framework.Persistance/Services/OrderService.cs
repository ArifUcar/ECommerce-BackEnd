using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Domain.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services;

public sealed class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderDetail> _orderDetailRepository;
    private readonly IMapper _mapper;
    private readonly ILogService _logger;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IMapper mapper,
        ILogService logger)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateAsync(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = new Order
            {
                UserId = request.UserId,
                OrderStatusId = request.OrderStatusId,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.UtcNow,
                OrderDetails = request.OrderDetails.Select(detail => new OrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                }).ToList()
            };
            
            await _orderRepository.AddAsync(order, cancellationToken);
            await _logger.LogInfo($"Order created for user: {request.UserId}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error creating order for user: {request.UserId}");
            throw;
        }
    }

    public async Task<List<OrderDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetAllWithIncludeAsync(
                query => query
                    .Include(o => o.User)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product),
                cancellationToken);

            return _mapper.Map<List<OrderDto>>(orders);
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Error getting all orders");
            throw;
        }
    }

    public async Task<OrderDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetFirstWithIncludeAsync(
            x => x.Id == id,
            query => query
                .Include(o => o.User)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product),
            cancellationToken);

        if (order is null)
            throw new Exception("Sipariş bulunamadı!");

        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        try
        {
            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _logger.LogInfo($"Order updated: {order.Id}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error updating order: {order.Id}");
            throw;
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(id.ToString(), cancellationToken);
            if (order is null)
                throw new Exception("Sipariş bulunamadı!");

            await _orderRepository.DeleteAsync(order, cancellationToken);
            await _logger.LogInfo($"Order deleted: {id}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error deleting order: {id}");
            throw;
        }
    }
} 