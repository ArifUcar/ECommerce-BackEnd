using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _authService.RefreshTokenAsync(
            request.RefreshToken,
            cancellationToken);

        return new LoginResponse(token, refreshToken);
    }
} 