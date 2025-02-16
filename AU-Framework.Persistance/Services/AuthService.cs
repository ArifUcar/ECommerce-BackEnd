using AU_Framework.Application.Repository;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services;

public sealed class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogService _logger;
    private readonly IPasswordService _passwordService;

    public AuthService(
        IRepository<User> userRepository,
        IRepository<Role> roleRepository,
        IConfiguration configuration,
        ILogService logger,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _configuration = configuration;
        _logger = logger;
        _passwordService = passwordService;
    }

    public async Task<bool> RegisterAsync(
        User user,
        string password,
        List<string> roleNames,
        CancellationToken cancellationToken)
    {
        try
        {
            await _logger.LogInfo($"Registration attempt for email: {user.Email}");

            var existingUser = await _userRepository.GetFirstAsync(
                x => x.Email == user.Email,
                cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("Bu email adresi zaten kayıtlı!");
            }

            var validationResult = _passwordService.ValidatePassword(password);
            if (!validationResult.IsValid)
            {
                throw new Exception(string.Join("\n", validationResult.Errors));
            }

            user.Password = _passwordService.HashPassword(password);

            if (roleNames.Any())
            {
                var roles = await _roleRepository.FindAsync(
                    x => roleNames.Contains(x.Name),
                    cancellationToken);

                user.Roles = roles.ToList();
            }

            await _userRepository.AddAsync(user, cancellationToken);
            await _logger.LogInfo($"User registered successfully: {user.Email}");

            return true;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Registration failed for email: {user.Email}");
            throw;
        }
    }

    public async Task<(string Token, string RefreshToken)> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        try
        {
            await _logger.LogInfo($"Login attempt for email: {email}");
            
            var user = await _userRepository.GetFirstWithIncludeAsync(
                x => x.Email == email,
                query => query.Include(u => u.Roles),
                cancellationToken);

            if (user is null)
            {
                await _logger.LogWarning($"Login failed: User not found for email: {email}");
                throw new Exception("Kullanıcı bulunamadı!");
            }

            if (!_passwordService.VerifyPassword(password, user.Password))
            {
                await _logger.LogWarning($"Login failed: Invalid password for email: {email}");
                throw new Exception("Şifre yanlış!");
            }

            if (!user.IsActive)
                throw new Exception("Kullanıcı hesabı aktif değil!");

            if (user.Roles == null || !user.Roles.Any())
            {
                await _logger.LogWarning($"User has no roles: {email}");
                throw new Exception("Kullanıcı rolü bulunamadı!");
            }

            string token = GenerateJwtToken(user);
            user.GenerateRefreshToken();
            await _userRepository.UpdateAsync(user, cancellationToken);

            await _logger.LogInfo($"Login successful for user: {user.Email}");
            return (token, user.RefreshToken!);
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex, $"Login failed for email: {email}");
            throw;
        }
    }

    public async Task<(string Token, string RefreshToken)> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstWithIncludeAsync(
            x => x.RefreshToken == refreshToken,
            query => query.Include(u => u.Roles),
            cancellationToken);
        
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

        if (!_passwordService.VerifyPassword(currentPassword, user.Password))
            throw new Exception("Mevcut şifre yanlış!");

        user.Password = _passwordService.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user, cancellationToken);
        
        await _logger.LogInfo($"Password changed for user: {user.Email}");
        return true;
    }

    public async Task<bool> ResetPasswordAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(
            predicate: x => x.Email == email,
            cancellationToken: cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        // TODO: Reset token doğrulaması yapılmalı
        
        user.Password = _passwordService.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user, cancellationToken);
        
        await _logger.LogInfo($"Password reset for user: {user.Email}");
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstAsync(
            predicate: x => x.Email == email,
            cancellationToken: cancellationToken);
        
        if (user is null)
            throw new Exception("Kullanıcı bulunamadı!");

        // TODO: Şifre sıfırlama maili gönderme işlemi
        
        await _logger.LogInfo($"Password reset requested for user: {user.Email}");
        return true;
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);

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
        
        await _logger.LogInfo($"Token revoked for user: {user.Email}");
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        if (user.Roles != null)
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}