namespace E_Commerce.Environments;

public static class EnvironmentHelper
{
    private static IConfiguration? _configuration;
    private static IWebHostEnvironment? _environment;

    public static void Initialize(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public static string ApiBaseUrl
    {
        get
        {
            if (_environment?.IsDevelopment() ?? false)
                return Development.ApiBaseUrl;
            
            if (_environment?.IsStaging() ?? false)
                return Staging.ApiBaseUrl;
            
            return Production.ApiBaseUrl;
        }
    }

    public static string AuthScheme
    {
        get
        {
            if (_environment?.IsDevelopment() ?? false)
                return Development.AuthScheme;
            
            if (_environment?.IsStaging() ?? false)
                return Staging.AuthScheme;
            
            return Production.AuthScheme;
        }
    }

    public static string GetConnectionString(string name)
    {
        return _configuration?.GetConnectionString(name) 
            ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
    }

    public static string GetValue(string key)
    {
        return _configuration?[key] 
            ?? throw new InvalidOperationException($"Configuration value '{key}' not found.");
    }
} 