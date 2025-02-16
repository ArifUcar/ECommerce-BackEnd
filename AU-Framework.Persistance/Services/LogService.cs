using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Application.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AU_Framework.Persistance.Services;

public sealed class LogService : ILogService
{
    private readonly IRepository<Log> _logRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogService(
        IRepository<Log> logRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _logRepository = logRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogInfo(string message, string memberName = "")
    {
        await CreateLog("Information", message, null, memberName);
    }

    public async Task LogWarning(string message, string memberName = "")
    {
        await CreateLog("Warning", message, null, memberName);
    }

    public async Task LogError(Exception ex, string message, string memberName = "")
    {
        await CreateLog("Error", message, ex, memberName);
    }

    public async Task LogDebug(string message, string memberName = "")
    {
        await CreateLog("Debug", message, null, memberName);
    }

    private async Task CreateLog(string level, string message, Exception? exception = null, string memberName = "")
    {
        var context = _httpContextAccessor.HttpContext;
        var userId = context?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = context?.User.FindFirst(ClaimTypes.Name)?.Value;

        var logEntry = new Log
        {
            Level = level,
            Message = message,
            Exception = exception?.ToString(),
            MethodName = memberName,
            RequestPath = context?.Request.Path.Value,
            RequestMethod = context?.Request.Method,
            RequestBody = await GetRequestBody(context?.Request),
            UserId = userId,
            UserName = userName,
            CreatedDate = DateTime.UtcNow
        };

        await _logRepository.AddAsync(logEntry, CancellationToken.None);
    }

    private async Task<string?> GetRequestBody(HttpRequest? request)
    {
        if (request == null) return null;

        try
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body.Length > 4000 ? body[..4000] : body;
        }
        catch
        {
            return null;
        }
    }
} 