using AU_Framework.Application.Features.AuthFeatures.Commands.ChangePassword;
using AU_Framework.Application.Features.AuthFeatures.Commands.ForgotPassword;
using AU_Framework.Application.Features.AuthFeatures.Commands.Login;
using AU_Framework.Application.Features.AuthFeatures.Commands.RefreshToken;
using AU_Framework.Application.Features.AuthFeatures.Commands.Register;
using AU_Framework.Application.Features.AuthFeatures.Commands.ResetPassword;
using AU_Framework.Presentation.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AU_Framework.Presentation.Controllers;

public sealed class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RefreshTokenCommand(refreshToken), cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ForgotPasswordCommand(email), cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("[action]")]
    [Authorize]
    public IActionResult TestAuth()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        
        return Ok(new { Message = "Authorized", UserId = userId, Email = userEmail });
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        // Role atama işlemi
        return Ok();
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUsers()
    {
        // Kullanıcı listesi
        return Ok();
    }
} 