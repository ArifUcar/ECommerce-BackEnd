namespace AU_Framework.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommandResponse(
    string Token,
    string RefreshToken,
    string Email,
    string FirstName,
    string LastName,
    string UserId,
    List<string> Roles
); 