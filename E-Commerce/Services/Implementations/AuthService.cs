using E_Commerce.Models;  // ApiResponse için
using E_Commerce.Models.Auth;
using E_Commerce.Services.Interfaces;
using E_Commerce.Environments;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace E_Commerce.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "auth_token";
    private string? _cachedToken;


    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public bool IsAuthenticated => !string.IsNullOrEmpty(_cachedToken);

    public async Task<ApiResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            Console.WriteLine($"Login attempt with email: {request.Email}"); // Debug için

            var loginData = new Dictionary<string, string>
            {
                { "Email", request.Email },
                { "Password", request.Password }
            };

            var response = await _httpClient.PostAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Auth/Login", loginData);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {responseContent}"); // Debug için

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                if (result?.Success == true && !string.IsNullOrEmpty(result.Token))
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, result.Token);
                    _cachedToken = result.Token;
                }
                return result ?? new ApiResponse { Success = false, Message = "Beklenmeyen bir hata oluştu" };
            }
            
            return new ApiResponse 
            { 
                Success = false, 
                Message = $"Giriş başarısız: {responseContent}" 
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}"); // Debug için
            return new ApiResponse 
            { 
                Success = false, 
                Message = $"Giriş yapılırken hata oluştu: {ex.Message}" 
            };
        }
    }

    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Auth/Register", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return result?.Message ?? "Kayıt başarıyla tamamlandı";
    }

    public async Task<bool> LogoutAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            _cachedToken = null;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetCurrentUserTokenAsync()
    {
        if (!string.IsNullOrEmpty(_cachedToken))
            return _cachedToken;

        try
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
            if (string.IsNullOrEmpty(token))
                throw new Exception("Token bulunamadı");

            _cachedToken = token;
            return token;
        }
        catch
        {
            throw new Exception("Oturum açmanız gerekmektedir");
        }
    }
} 