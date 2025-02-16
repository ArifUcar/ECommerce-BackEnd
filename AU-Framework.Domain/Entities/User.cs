using AU_Framework.Domain.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace AU_Framework.Domain.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    private string _password;
    public string Password 
    { 
        get => _password;
        set => _password = HashPassword(value);
    }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime LastLoginDate { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<Role> Roles { get; set; }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password)
    {
        return HashPassword(password) == _password;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public void GenerateRefreshToken()
    {
        RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        RefreshTokenExpires = DateTime.Now.AddDays(7);
    }

    public bool IsRefreshTokenValid()
    {
        return RefreshToken != null && 
               RefreshTokenExpires.HasValue && 
               RefreshTokenExpires.Value > DateTime.Now;
    }
}
