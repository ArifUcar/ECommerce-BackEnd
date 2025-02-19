using E_Commerce.Models;
using E_Commerce.Models.Auth;

namespace E_Commerce.Services.Interfaces;

public interface IAuthService
{
    bool IsAuthenticated { get; }
    Task<ApiResponse> LoginAsync(LoginRequest request);
    Task<string> RegisterAsync(RegisterRequest request);
    Task<bool> LogoutAsync();
    Task<string> GetCurrentUserTokenAsync();
} 