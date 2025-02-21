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
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogService _logger;
    private readonly ICurrentUser _currentUser;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository,
        IMapper mapper,
        ILogService logger,
        ICurrentUser currentUser)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task CreateAsync(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedAccessException("Kullanıcı giriş yapmamış!");

            var order = new Order
            {
                UserId = _currentUser.UserId,
                OrderStatusId = request.OrderStatusId,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };

            // Her bir ürün için detay oluştur
            foreach (var detail in request.OrderDetails)
            {
                var product = await _productRepository.GetFirstAsync(
                    x => x.Id == detail.ProductId && !x.IsDeleted, 
                    cancellationToken);

                if (product == null)
                    throw new Exception($"Ürün bulunamadı: {detail.ProductId}");

                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = detail.ProductId,
                    ProductName = product.ProductName,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    SubTotal = detail.Quantity * detail.UnitPrice
                });
            }
            
            await _orderRepository.AddAsync(order, cancellationToken);
            await _logger.LogInfo($"Order created for user: {_currentUser.UserId}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error creating order for user: {_currentUser.UserId}");
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
                    .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                    .Where(o => !o.IsDeleted)
                    .AsNoTracking(),
                cancellationToken);

            if (orders == null || !orders.Any())
                return new List<OrderDto>();

            var orderDtos = orders.Select(order => new OrderDto(
                order.Id,
                order.UserId,
                order.User != null ? $"{order.User.FirstName} {order.User.LastName}" : string.Empty,
                order.OrderDate,
                order.TotalAmount,
                order.OrderStatus != null ? order.OrderStatus.Name : string.Empty,
                order.OrderDetails.Select(detail => new OrderDetailDto(
                    detail.Id,
                    detail.ProductId,
                    detail.Product != null ? detail.Product.ProductName : string.Empty,
                    detail.Quantity,
                    detail.UnitPrice
                )).ToList()
            )).ToList();

            return orderDtos;
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

        // 🛠 Doğru atamalar
        order.OrderDate = request.OrderDate;
        order.OrderStatusId = request.OrderStatusId;
        order.TotalAmount = request.TotalAmount;

        // 🛠 Eğer `UserId` de güncellenmek isteniyorsa
        order.UserId = request.UserId;

  

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