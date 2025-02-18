using E_Commerce.Models.Auth;

namespace E_Commerce.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<string> RegisterAsync(RegisterRequest request);
    Task<bool> LogoutAsync();
    Task<string> GetCurrentUserTokenAsync();
    bool IsAuthenticated { get; }
} 