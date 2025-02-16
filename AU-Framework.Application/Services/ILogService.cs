using System.Runtime.CompilerServices;

namespace AU_Framework.Application.Services;

public interface ILogService
{
    Task LogInfo(string message, [CallerMemberName] string memberName = "");
    Task LogWarning(string message, [CallerMemberName] string memberName = "");
    Task LogError(Exception ex, string message, [CallerMemberName] string memberName = "");
    Task LogDebug(string message, [CallerMemberName] string memberName = "");
} 