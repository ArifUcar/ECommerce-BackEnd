using AU_Framework.Application.Services;
using System.Diagnostics;

namespace AU_Framework.WebAPI.Middleware;

public class RequestLoggingMiddleware : IMiddleware
{
    private readonly ILogService _logger;

    public RequestLoggingMiddleware(ILogService logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var requestBody = await GetRequestBody(context.Request);
            await _logger.LogInfo($"HTTP {context.Request.Method} {context.Request.Path} started. Request: {requestBody}");

            await next(context);

            sw.Stop();
            await _logger.LogInfo($"HTTP {context.Request.Method} {context.Request.Path} completed in {sw.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            sw.Stop();
            await _logger.LogError(ex, $"HTTP {context.Request.Method} {context.Request.Path} failed in {sw.ElapsedMilliseconds}ms");
            throw;
        }
    }

    private async Task<string> GetRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }
} 