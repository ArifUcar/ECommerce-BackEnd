using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : IRequest<LoginResponse>; 