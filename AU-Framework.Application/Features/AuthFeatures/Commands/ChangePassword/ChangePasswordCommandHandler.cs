using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;

    public ChangePasswordCommandHandler(
        IAuthService authService,
        ICurrentUser currentUser)
    {
        _authService = authService;
        _currentUser = currentUser;
    }

    public async Task<MessageResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
            throw new Exception("Kullanıcı oturum açmamış!");

        await _authService.ChangePasswordAsync(
            _currentUser.UserId,
            request.CurrentPassword,
            request.NewPassword,
            cancellationToken);

        return new MessageResponse("Şifre başarıyla değiştirildi.");
    }
} 