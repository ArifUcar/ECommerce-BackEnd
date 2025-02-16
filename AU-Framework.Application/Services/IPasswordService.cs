using AU_Framework.Domain.Dtos;

namespace AU_Framework.Application.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    PasswordValidationResult ValidatePassword(string password);
} 