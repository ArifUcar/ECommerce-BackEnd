using AU_Framework.Domain.Dtos;
using MediatR;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Address
) : IRequest<MessageResponse>; 