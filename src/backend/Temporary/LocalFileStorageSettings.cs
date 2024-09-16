namespace Temporary;

/// <summary>
/// Represents the local storage settings.
/// </summary>
public sealed record LocalFileStorageSettings
{
    /// <summary>
    /// Gets the root path for local storage.
    /// </summary>
    public string RootPath { get; set; } = default!;
}