using AU_Framework.Domain.Abstract;

namespace AU_Framework.Domain.Entities;

public sealed class Log : BaseEntity
{
    public string Level { get; set; }
    public string Message { get; set; }
    public string? Exception { get; set; }
    public string? MethodName { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestMethod { get; set; }
    public long? ExecutionTime { get; set; }
    public string? RequestBody { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
} 