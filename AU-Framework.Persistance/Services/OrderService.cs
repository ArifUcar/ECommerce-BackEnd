using AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;
using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Domain.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AU_Framework.Application.Features.OrderFeatures.Commands.DeleteOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrder;
using AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrderStatus;
using AU_Framework.Application.Features.OrderFeatures.Queries;
using AU_Framework.Application.Features.OrderFeatures.Queries.GetAllOrders;
using System.Linq;

namespace AU_Framework.Persistance.Services;

public sealed class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IProductStockService _productStockService;
    private readonly IMapper _mapper;
    private readonly ILogService _logger;
    private readonly ICurrentUser _currentUser;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository,
        IProductStockService productStockService,
        IMapper mapper,
        ILogService logger,
        ICurrentUser currentUser)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _productStockService = productStockService;
        _mapper = mapper;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task CancelOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetFirstWithIncludeAsync(
                x => x.Id == orderId && !x.IsDeleted,
                query => query
                    .Include(o => o.OrderDetails)
                    .Include(o => o.OrderStatus),
                cancellationToken);

            if (order == null)
                throw new Exception("Sipariş bulunamadı!");

            // İptal edilmiş veya tamamlanmış siparişler iptal edilemez
            if (order.OrderStatus.Name == "İptal Edildi")
                throw new Exception("Bu sipariş zaten iptal edilmiş!");

            if (order.OrderStatus.Name == "Tamamlandı")
                throw new Exception("Tamamlanmış sipariş iptal edilemez!");

            // Stokları geri yükle
            foreach (var detail in order.OrderDetails)
            {
                var product = await _productRepository.GetFirstAsync(
                    x => x.Id == detail.ProductId && !x.IsDeleted,
                    cancellationToken);

                if (product != null)
                {
                    product.StockQuantity += detail.Quantity;
                    await _productRepository.UpdateAsync(product, cancellationToken);
                    await _logger.LogInfo($"Stock restored for product {product.ProductName}. New stock: {product.StockQuantity}");
                }
            }

            // Sipariş durumunu güncelle
            order.OrderStatusId = new Guid("df7579ee-4af9-4b71-9ada-7f792f76921d"); // İptal Edildi status ID'si
            await _orderRepository.UpdateAsync(order, cancellationToken);
            
            await _logger.LogInfo($"Order cancelled and stocks restored. OrderId: {orderId}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error cancelling order: {orderId}");
            throw;
        }
    }

    public async Task CreateAsync(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedAccessException("Kullanıcı giriş yapmamış!");

            // Ürünlerin stok kontrolü
            foreach (var detail in request.OrderDetails)
            {
                var isAvailable = await _productStockService.CheckStockAvailability(
                    detail.ProductId, 
                    detail.Quantity, 
                    cancellationToken);

                if (!isAvailable)
                    throw new Exception($"Yetersiz stok. Ürün ID: {detail.ProductId}");
            }

            var order = new Order
            {
                UserId = _currentUser.UserId,
                OrderStatusId = request.OrderStatusId,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.UtcNow,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                ShippingAddress = request.ShippingAddress,
                City = request.City,
                District = request.District,
                ZipCode = request.ZipCode,
                OrderDetails = new List<OrderDetail>()
            };

            // Siparişi oluştur ve stokları güncelle
            foreach (var detail in request.OrderDetails)
            {
                await _productStockService.UpdateStock(
                    detail.ProductId, 
                    detail.Quantity, 
                    cancellationToken);

                var product = await _productRepository.GetFirstAsync(
                    x => x.Id == detail.ProductId && !x.IsDeleted, 
                    cancellationToken);

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

    public async Task<List<GetAllOrdersQueryResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetAllWithIncludeAsync(
                query => query
                    .Include(o => o.User)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => !o.IsDeleted)
                    .OrderByDescending(o => o.OrderDate),
                cancellationToken);

            return await orders.Select(order => new GetAllOrdersQueryResponse(
                order.Id,
                order.UserId,
                $"{order.User.FirstName} {order.User.LastName}",
                order.OrderDate,
                order.TotalAmount,
                order.OrderStatus.Name,
                order.CustomerName,
                order.CustomerPhone,
                order.ShippingAddress,
                order.City,
                order.District,
                order.ZipCode,
                order.CreatedDate,
                order.UpdatedDate,
                order.IsDeleted,
                order.OrderDetails.Select(detail => new OrderDetailResponse(
                    detail.Id,
                    detail.ProductId,
                    detail.ProductName,
                    detail.Quantity,
                    detail.UnitPrice,
                    detail.SubTotal,
                    detail.CreatedDate,
                    detail.UpdatedDate,
                    detail.IsDeleted
                )).ToList()
            )).ToListAsync(cancellationToken);
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

    public async Task<List<OrderDto>> GetUserOrdersAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (!_currentUser.IsAuthenticated)
                throw new UnauthorizedAccessException("Kullanıcı giriş yapmamış!");

            var orders = await _orderRepository.GetAllWithIncludeAsync(
                query => query
                    .Include(o => o.User)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.OrderDetails)
                    .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                    .Where(o => o.UserId == _currentUser.UserId && !o.IsDeleted)
                    .OrderByDescending(o => o.OrderDate)
                    .AsNoTracking(),
                cancellationToken);

            if (orders == null || !orders.Any())
                return new List<OrderDto>();

            var orderDtos = orders.Select(order => new OrderDto(
                order.Id,
                order.UserId,
                $"{order.User.FirstName} {order.User.LastName}",
                order.OrderDate,
                order.TotalAmount,
                order.OrderStatus.Name,
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
            await _logger.LogError(ex, $"Error getting orders for user: {_currentUser.UserId}");
            throw;
        }
    }

    public async Task<int> GetOrderCountAsync(CancellationToken cancellationToken)
    {
        try
        {
            var query = await _orderRepository.GetAllAsync(cancellationToken);
            return await query.Where(o => !o.IsDeleted).CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Error getting order count");
            throw;
        }
    }

    public async Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken)
    {
        try
        {
            var query = await _orderRepository.GetAllWithIncludeAsync(
                include => include
                    .Include(o => o.OrderStatus)
                    .Where(o => !o.IsDeleted && o.OrderStatus.Name != "İptal Edildi"),
                cancellationToken);

            return await query.SumAsync(o => o.TotalAmount, cancellationToken);
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, "Error calculating total revenue");
            throw;
        }
    }

    public async Task UpdateOrderStatusAsync(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetFirstWithIncludeAsync(
                x => x.Id == request.OrderId && !x.IsDeleted,
                query => query
                    .Include(o => o.OrderStatus),
                cancellationToken);

            if (order == null)
                throw new Exception("Sipariş bulunamadı!");

            // Tamamlanmış siparişlerin durumu değiştirilemez
            if (order.OrderStatus.Name == "Tamamlandı")
                throw new Exception("Tamamlanmış siparişin durumu değiştirilemez!");

            // İptal edilmiş siparişlerin durumu değiştirilemez
            if (order.OrderStatus.Name == "İptal Edildi")
                throw new Exception("İptal edilmiş siparişin durumu değiştirilemez!");

            order.OrderStatusId = request.OrderStatusId;
            await _orderRepository.UpdateAsync(order, cancellationToken);
            
            await _logger.LogInfo($"Order status updated. OrderId: {request.OrderId}, New StatusId: {request.OrderStatusId}");
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Error updating order status. OrderId: {request.OrderId}");
            throw;
        }
    }

 
} 