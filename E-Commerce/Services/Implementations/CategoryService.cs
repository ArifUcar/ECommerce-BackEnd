using E_Commerce.Models;
using E_Commerce.Services.Interfaces;
using E_Commerce.Environments;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace E_Commerce.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;

    public CategoryService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var response = await _httpClient.GetAsync($"{EnvironmentHelper.ApiBaseUrl}/Category/GetAll");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Category>>() ?? new List<Category>();
    }

    public async Task<Category> GetCategoryByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{EnvironmentHelper.ApiBaseUrl}/Category/GetById/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Category>() 
            ?? throw new Exception("Kategori bulunamadı");
    }

    public async Task<string> CreateCategoryAsync(Category category)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Category/Create", category);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Kategori başarıyla oluşturuldu";
    }

    public async Task<string> UpdateCategoryAsync(Category category)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Category/Update", category);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Kategori başarıyla güncellendi";
    }

    public async Task<string> DeleteCategoryAsync(Guid id)
    {
        var token = await _authService.GetCurrentUserTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"{EnvironmentHelper.ApiBaseUrl}/Category/Delete/{id}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Kategori başarıyla silindi";
    }
} 