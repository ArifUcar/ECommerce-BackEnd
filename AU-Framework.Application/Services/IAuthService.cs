using AU_Framework.Domain.Entities;

namespace AU_Framework.Application.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(User user, string password, List<string> roleNames, CancellationToken cancellationToken);
    Task<(string Token, string RefreshToken)> LoginAsync(string email, string password, CancellationToken cancellationToken);
    Task<bool> ValidateTokenAsync(string token);
    Task<(string Token, string RefreshToken)> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<bool> RevokeTokenAsync(string userId, CancellationToken cancellationToken);

    Task<bool> ChangePasswordAsync(
        string userId, 
        string currentPassword, 
        string newPassword, 
        CancellationToken cancellationToken);

    Task<bool> ResetPasswordAsync(
        string email, 
        string token, 
        string newPassword, 
        CancellationToken cancellationToken);

    Task<bool> ForgotPasswordAsync(
        string email, 
        CancellationToken cancellationToken);
} 