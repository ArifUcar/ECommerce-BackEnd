namespace AU_Framework.Application.Services;

public interface ICurrentUser
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
} 