using AU_Framework.Application.Services;
using AU_Framework.Domain.Dtos;
using AU_Framework.Domain.Entities;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address
        };

        await _authService.RegisterAsync(user, request.Password, cancellationToken);
        return new MessageResponse("Kullanıcı başarıyla kaydedildi");
    }
} 