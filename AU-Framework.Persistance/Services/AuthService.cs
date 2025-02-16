using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AU_Framework.Persistance.Services;

public sealed class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IRepository<User> userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<(string Token, string RefreshToken)> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(x => x.Email == email, cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        if (!user.VerifyPassword(password))
            throw new Exception("Şifre yanlış!");

        if (!user.IsActive)
            throw new Exception("Kullanıcı hesabı aktif değil!");

        string token = GenerateJwtToken(user);
        user.GenerateRefreshToken();
        await _userRepository.UpdateAsync(user, cancellationToken);

        return (token, user.RefreshToken!);
    }

    public async Task<bool> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetFirstAsync(x => x.Email == user.Email, cancellationToken);
        if (existingUser is not null)
            throw new Exception("Bu email adresi zaten kayıtlı!");

        user.Password = password; // Password property'si içinde hash'leme yapılıyor
        user.IsActive = true;
        user.LastLoginDate = DateTime.UtcNow;

        await _userRepository.AddAsync(user, cancellationToken);
        return true;
    }

    public async Task<(string Token, string RefreshToken)> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(x => x.RefreshToken == refreshToken, cancellationToken);
        
        if (user is null || !user.IsRefreshTokenValid())
            throw new Exception("Geçersiz refresh token!");

        string token = GenerateJwtToken(user);
        user.GenerateRefreshToken();
        await _userRepository.UpdateAsync(user, cancellationToken);

        return (token, user.RefreshToken!);
    }

    public async Task<bool> ChangePasswordAsync(
        string userId,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        if (!user.VerifyPassword(currentPassword))
            throw new Exception("Mevcut şifre yanlış!");

        user.Password = newPassword;
        await _userRepository.UpdateAsync(user, cancellationToken);
        return true;
    }

    public async Task<bool> ResetPasswordAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(x => x.Email == email, cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        // TODO: Reset token doğrulaması yapılmalı
        
        user.Password = newPassword;
        await _userRepository.UpdateAsync(user, cancellationToken);
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(x => x.Email == email, cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        // TODO: Şifre sıfırlama maili gönderme işlemi
        
        return true;
    }

    public Task<bool> ValidateTokenAsync(
        string token,
        CancellationToken cancellationToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out _);

            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public async Task<bool> RevokeTokenAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        user.RefreshToken = null;
        user.RefreshTokenExpires = null;
        await _userRepository.UpdateAsync(user, cancellationToken);
        
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.GetFullName()),
            new(ClaimTypes.Role, "User"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 