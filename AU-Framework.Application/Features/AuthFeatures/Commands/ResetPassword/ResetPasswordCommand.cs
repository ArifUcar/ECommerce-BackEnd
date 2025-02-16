using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword
) : IRequest<MessageResponse>; 