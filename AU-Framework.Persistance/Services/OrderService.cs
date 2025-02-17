using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Domain.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AU_Framework.Application.Features.OrderFeatures.Commands.DeleteOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;

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

    public async Task UpdateAsync(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (order == null)
            throw new Exception("Sipariş bulunamadı!"); // ✅ Hata mesajı düzeltildi.

        // ✅ Doğru mapleme işlemi
        _mapper.Map(request, order);

        await _orderRepository.UpdateAsync(order, cancellationToken);
    }


    public async Task DeleteAsync(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Order? order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order == null)
            {
                throw new Exception("Sipariş bulunamadı");
            }
            order.IsDeleted = true;
            await _orderRepository.UpdateAsync(order, cancellationToken);

        }
        catch (Exception ex) when (ex.Message=="Geçersiz ID formatı!")
        {
            throw new Exception("Geçersiz kategori ID'si formatı!");
        }
        catch (Exception)
        {
            throw new Exception("Sipariş silme işlemi sırasında bir hata oluştu!");
        }
    }
} 