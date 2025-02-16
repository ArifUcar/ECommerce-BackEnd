using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginCommandResponse>; 