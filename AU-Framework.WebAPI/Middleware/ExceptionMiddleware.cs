#nullable enable

using AU_Framework.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace AU_Framework.WebAPI.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogService _logger;

    public ExceptionMiddleware(ILogService logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (ArgumentException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message,
            Path = context.Request.Path,
            Method = context.Request.Method,
            Timestamp = DateTime.UtcNow
        };

        // Development ortamında daha detaylı hata bilgisi
        if (context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() ?? false)
        {
            errorResponse.StackTrace = ex.StackTrace;
            errorResponse.Source = ex.Source;
            errorResponse.InnerException = ex.InnerException?.Message;
        }

        // Hatayı logla
        var logMessage = $"HTTP {context.Request.Method} {context.Request.Path} failed with status code {statusCode}";
        await _logger.LogError(ex, logMessage);

        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }
    public string Method { get; set; }
    public DateTime Timestamp { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? InnerException { get; set; }
}
