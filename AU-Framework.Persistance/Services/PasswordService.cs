using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AU_Framework.Persistance.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithmName,
            KeySize);

        return string.Join(Delimiter, 
            Convert.ToBase64String(salt), 
            Convert.ToBase64String(hash));
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var elements = hashedPassword.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            _hashAlgorithmName,
            KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }

    public PasswordValidationResult ValidatePassword(string password)
    {
        var result = new PasswordValidationResult();

        if (string.IsNullOrWhiteSpace(password))
        {
            result.Errors.Add("Parola boş olamaz.");
            result.IsValid = false;
            return result;
        }

        var validators = new List<(Func<string, bool> Validator, string ErrorMessage)>
        {
            (p => p.Length >= 8, "Parola en az 8 karakter uzunluğunda olmalıdır."),
            (p => p.Length <= 32, "Parola 32 karakterden uzun olamaz."),
            (p => Regex.IsMatch(p, @"[A-Z]"), "Parola en az bir büyük harf içermelidir."),
            (p => Regex.IsMatch(p, @"[a-z]"), "Parola en az bir küçük harf içermelidir."),
            (p => Regex.IsMatch(p, @"[0-9]"), "Parola en az bir rakam içermelidir."),
            (p => Regex.IsMatch(p, @"[!@#$%^&*(),.?""':{}|<>]"), "Parola en az bir özel karakter içermelidir."),
            (p => !Regex.IsMatch(p, @"\s"), "Parola boşluk karakteri içeremez."),
        };

        foreach (var (validator, errorMessage) in validators)
        {
            if (!validator(password))
            {
                result.Errors.Add(errorMessage);
            }
        }

        result.IsValid = !result.Errors.Any();
        return result;
    }
} 