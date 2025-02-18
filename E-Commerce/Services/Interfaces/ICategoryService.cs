using E_Commerce.Models;

namespace E_Commerce.Services.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(Guid id);
    Task<string> CreateCategoryAsync(Category category);
    Task<string> UpdateCategoryAsync(Category category);
    Task<string> DeleteCategoryAsync(Guid id);
} 