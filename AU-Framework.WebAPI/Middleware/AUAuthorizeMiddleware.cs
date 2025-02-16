using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AU_Framework.WebAPI.Middleware;

public class AUAuthorizeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AUAuthorizeMiddleware> _logger;

    public AUAuthorizeMiddleware(RequestDelegate next, ILogger<AUAuthorizeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await _next(context);
                return;
            }

            var authorizeAttributes = endpoint.Metadata
                .OfType<AuthorizeAttribute>()
                .ToList();

            if (!authorizeAttributes.Any())
            {
                await _next(context);
                return;
            }

            var user = context.User;
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                _logger.LogWarning("Unauthorized access attempt");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Yetkilendirme başarısız. Lütfen giriş yapın." });
                return;
            }

            var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            foreach (var attribute in authorizeAttributes)
            {
                var requiredRoles = attribute.Roles?.Split(',').Select(r => r.Trim()).ToList();
                if (requiredRoles != null && !requiredRoles.Any(role => userRoles.Contains(role)))
                {
                    _logger.LogWarning($"Access denied. User roles: {string.Join(", ", userRoles)}, Required roles: {string.Join(", ", requiredRoles)}");
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { message = "Bu işlem için yetkiniz yok." });
                    return;
                }
            }

            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            _logger.LogInformation(
                "Access granted to {Controller}.{Action} for user {UserId} with roles {Roles}",
                controllerActionDescriptor?.ControllerName,
                controllerActionDescriptor?.ActionName,
                user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                string.Join(", ", userRoles));

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in authorization middleware");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { message = "Yetkilendirme işlemi sırasında bir hata oluştu." });
        }
    }
}

// Extension method
public static class AUAuthorizeMiddlewareExtensions
{
    public static IApplicationBuilder UseAUAuthorize(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AUAuthorizeMiddleware>();
    }
} 