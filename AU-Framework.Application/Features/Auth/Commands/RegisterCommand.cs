using MediatR;
using System.Collections.Generic;

namespace AU_Framework.Application.Features.Auth.Commands
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Phone,
        string Address,
        List<string>? Roles = null) : IRequest<bool>;
} 