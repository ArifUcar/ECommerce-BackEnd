namespace AU_Framework.Domain.Dtos;

public class PasswordValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
} 