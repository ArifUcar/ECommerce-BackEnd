using AU_Framework.Domain.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace AU_Framework.Domain.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime LastLoginDate { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<Role> Roles { get; set; }

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

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
