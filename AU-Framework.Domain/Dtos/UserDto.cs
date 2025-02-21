namespace AU_Framework.Domain.Dtos;

public sealed record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime CreatedDate,
    DateTime LastLoginDate,
    bool IsActive,
    List<string> Roles); 