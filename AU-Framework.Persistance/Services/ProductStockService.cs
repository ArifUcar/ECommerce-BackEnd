using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace AU.Framework.Persistance.Services
{
    public class ProductStockService : IProductStockService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogService _logger;

        public ProductStockService(IRepository<Product> productRepository, ILogService logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> CheckStockAvailability(Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetFirstAsync(
                x => x.Id == productId && !x.IsDeleted,
                cancellationToken);

            if (product == null)
                throw new Exception($"Ürün bulunamadı: {productId}");

            return product.StockQuantity >= quantity;
        }

        public async Task UpdateStock(Guid productId, int quantity, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetFirstAsync(
                x => x.Id == productId && !x.IsDeleted,
                cancellationToken);

            if (product == null)
                throw new Exception($"Ürün bulunamadı: {productId}");

            if (product.StockQuantity < quantity)
                throw new Exception($"Yetersiz stok. Ürün: {product.ProductName}, Mevcut stok: {product.StockQuantity}, İstenen miktar: {quantity}");

            product.StockQuantity -= quantity;
            await _productRepository.UpdateAsync(product, cancellationToken);
            await _logger.LogInfo($"Stock updated for product {product.ProductName}. New stock: {product.StockQuantity}");
        }
    }
} 