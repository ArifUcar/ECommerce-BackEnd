using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email
) : IRequest<MessageResponse>; 