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

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"{EnvironmentHelper.ApiBaseUrl}/Auth/Login", request);
        response.EnsureSuccessStatusCode();
        
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (loginResponse == null)
            throw new Exception("Giriş başarısız");

        // Token'ı localStorage'a kaydet
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, loginResponse.Token);
        _cachedToken = loginResponse.Token;

        return loginResponse;
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