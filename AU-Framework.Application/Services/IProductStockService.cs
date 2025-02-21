public interface IProductStockService
{
    Task<bool> CheckStockAvailability(Guid productId, int quantity, CancellationToken cancellationToken = default);
    Task UpdateStock(Guid productId, int quantity, CancellationToken cancellationToken = default);
} 