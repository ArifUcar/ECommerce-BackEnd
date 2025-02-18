using E_Commerce.Models;
using E_Commerce.Services.Interfaces;
using E_Commerce.Environments;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace E_Commerce.Services.Implementations;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;

    public ProductService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/GetAll");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/GetById/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>() 
            ?? throw new Exception("Ürün bulunamadı");
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var response = await _httpClient.GetAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/GetByCategory/{categoryId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
    }

    public async Task<string> CreateProductAsync(Product product)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/Create", product);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Ürün başarıyla oluşturuldu";
    }

    public async Task<string> UpdateProductAsync(Product product)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/Update", product);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Ürün başarıyla güncellendi";
    }

    public async Task<string> DeleteProductAsync(Guid id)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"{EnvironmentHelper.ApiBaseUrl}/Product/Delete/{id}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Ürün başarıyla silindi";
    }
} 