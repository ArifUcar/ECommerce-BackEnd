namespace AU_Framework.Domain.Dtos;

public sealed record LoginResponse(
string Token,
string RefreshToken
);

