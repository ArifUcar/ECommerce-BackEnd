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
using Microsoft.Extensions.Logging;

namespace AU_Framework.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ApiController
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _mediator.Send(request, cancellationToken);
            
            // Token'ı response header'ına ekle
            Response.Headers.Add("Authorization", $"Bearer {response.Token}");
            
            _logger.LogInformation($"Successful login for user: {response.Email}");
            
            return Ok(new
            {
                token = response.Token,
                refreshToken = response.RefreshToken,
                user = new
                {
                    email = response.Email,
                    firstName = response.FirstName,
                    lastName = response.LastName,
                    userId = response.UserId,
                    roles = response.Roles
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Login failed for user: {request.Email}");
            return BadRequest(new { message = ex.Message });
        }
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
} 