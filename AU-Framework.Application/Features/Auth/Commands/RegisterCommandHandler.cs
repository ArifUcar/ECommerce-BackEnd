using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;

namespace AU_Framework.Application.Features.Auth.Commands
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };

            return await _authService.RegisterAsync(
                user: user,
                password: request.Password,
                roleNames: request.Roles ?? new List<string>(),
                cancellationToken: cancellationToken);
        }
    }
} 