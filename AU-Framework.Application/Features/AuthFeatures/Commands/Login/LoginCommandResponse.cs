public sealed record LoginCommandResponse(
    string Token,
    string Email,
    string FirstName,
    string LastName,
    string UserId,
    List<string> Roles
); 