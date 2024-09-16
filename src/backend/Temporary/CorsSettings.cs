namespace Temporary;

/// <summary>
/// Represents CORS settings
/// </summary>
public sealed record CorsSettings
{
    /// <summary>
    /// Gets collection of allowed origins
    /// </summary>
    public string[] AllowedOrigins { get; init; } = default!;
    
    /// <summary>
    /// Gets value indicating whether any origin is allowed
    /// </summary>
    public bool AllowAnyOrigins { get; init; }
    
    /// <summary>
    /// Gets value indicating whether any header is allowed
    /// </summary>
    public bool AllowAnyHeaders { get; init; }
    
    /// <summary>
    /// Gets value indicating whether any method is allowed
    /// </summary>
    public bool AllowAnyMethods { get; init; }
    
    /// <summary>
    /// Gets value indicating whether credentials are allowed
    /// </summary>
    public bool AllowCredentials { get; init; }
}