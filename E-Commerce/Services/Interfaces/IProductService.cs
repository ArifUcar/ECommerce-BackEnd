using E_Commerce.Models;

namespace E_Commerce.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task<string> CreateProductAsync(Product product);
    Task<string> UpdateProductAsync(Product product);
    Task<string> DeleteProductAsync(Guid id);
} 