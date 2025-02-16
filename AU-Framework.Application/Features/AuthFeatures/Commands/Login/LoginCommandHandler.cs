using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (token, refreshToken) = await _authService.LoginAsync(
            request.Email,
            request.Password,
            cancellationToken);

        return new LoginResponse(token, refreshToken);
    }
} 